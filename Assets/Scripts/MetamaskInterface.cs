using UnityEngine;
using System;
using MetaMask;
using MetaMask.Unity;
using Newtonsoft.Json;
using evm.net.Models;
using evm.net;
using Contracts;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine.Windows;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Events;

public class MetamaskInterface : MonoBehaviour
{
    private Dictionary<string, OddyMetadata> loadedMetadata = new Dictionary<string, OddyMetadata>();
    private Dictionary<string, Texture2D> loadedPFP = new Dictionary<string, Texture2D>();
    private MetaMaskWallet metaMaskWallet;
    private TOD721 todContract;

    [SerializeField]
    private string contractAddress = "0x71EAa691b6e5D5E75a3ebb36D4f87CBfb23C87b0";

    public UnityAction OnNFTDownloadComplete;

    static MetamaskInterface instance;

    public static MetamaskInterface Instance
    {
        get { return instance; }
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        metaMaskWallet = MetaMaskUnity.Instance.Wallet;
        todContract = Contract.Attach<TOD721>(metaMaskWallet, new EvmAddress(contractAddress));

        metaMaskWallet.WalletConnectedHandler += OnWalletConnected;
        metaMaskWallet.WalletAuthorizedHandler += OnWalletAuthorized;
        metaMaskWallet.AccountChangedHandler += OnAccountChanged;
        metaMaskWallet.WalletDisconnectedHandler += OnWalletDisconnected;
        metaMaskWallet.WalletReadyHandler += OnWalletReady;
        metaMaskWallet.EthereumRequestResultReceivedHandler += OnEthResultRecieved;
        metaMaskWallet.Connect();

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnWalletReady(object sender, EventArgs e)
    {
        var wallet = sender as MetaMaskWallet;
        //Clear the saved data as a different account will have different NFT's
        loadedPFP.Clear();
        loadedMetadata.Clear();

        //If the wallet is all good to get the data then get it
        if (wallet.IsAuthorized && wallet.IsConnected)
            TokenURITaskHandler();
    }

    private void OnWalletDisconnected(object sender, EventArgs e)
    {
        print($"Wallet Disconnected");
        loadedMetadata.Clear();
        loadedPFP.Clear();  
    }

    private void OnEthResultRecieved(object sender, MetaMaskEthereumRequestResultEventArgs e)
    {
        // print(e.Result.ToString());
    }
    private void OnAccountChanged(object sender, EventArgs e)
    {
        var wallet = sender as MetaMaskWallet;
    }

    private void OnWalletAuthorized(object sender, EventArgs e)
    {
        var wallet = sender as MetaMaskWallet;
        print("Wallet Authorized: " + wallet.SelectedAddress);
    }

    private void OnWalletConnected(object sender, EventArgs e)
    {
        var wallet = sender as MetaMaskWallet;
        print("Wallet Connected: " + wallet.SelectedAddress);
    }

    private async void TokenURITaskHandler()
    {
        LoadingScreen.Instance.Show("Gathering Oddys");//Too Coupled but fine for now
        var getTokensTask = todContract.TokensOfOwner(new EvmAddress(metaMaskWallet.ConnectedAddress));
        await getTokensTask;

        var tokenIds = getTokensTask.Result;

        //No Tokens in the wallet then return
        if (tokenIds.Length <= 1)
            return;

        //The 1st 2 integers are someother value that's not actual token data
        for (int i = 2; i < tokenIds.Length; i++)
        {
            string pfpPath = $"{Application.persistentDataPath}/PFP/{tokenIds[i].ToString()}.jpg";
            string jsonPath = $"{Application.persistentDataPath}/PFP/{tokenIds[i].ToString()}.json";

            var tokenURITask = todContract.TokenURI(tokenIds[i]);
            await tokenURITask;

            var localSerch = SearchForLocalToken(pfpPath, jsonPath);
            await localSerch;

            if (localSerch.Result == true)// this means that locally stored data has been found for this NFT
                continue;

            await SearchIPFSForToken(pfpPath, jsonPath, tokenURITask);

        }
        OnNFTDownloadComplete?.Invoke();
        LoadingScreen.Instance.Hide();//Too Coulpled but fine for now
    }

    private async Task SearchIPFSForToken(string pfpPath, string jsonPath, Task<string> tokenURITask)
    {
        var url = tokenURITask.Result.Replace("ipfs://", "https://ipfs.io/ipfs/");
        var request = UnityWebRequest.Get(url);
        await request.SendWebRequest();

        var jsonString = System.Text.ASCIIEncoding.UTF8.GetString(request.downloadHandler.data);
        var metadata = JsonConvert.DeserializeObject<OddyMetadata>(jsonString);
        var imgUrl = metadata.image.Replace("ipfs://", "https://ipfs.io/ipfs/");
        var pfpRequest = UnityWebRequestTexture.GetTexture(imgUrl);
        await pfpRequest.SendWebRequest();
        var pfpTexture = DownloadHandlerTexture.GetContent(pfpRequest);

        if (!Directory.Exists($"{Application.persistentDataPath}/PFP"))
            Directory.CreateDirectory($"{Application.persistentDataPath}/PFP");

        File.WriteAllBytes(pfpPath, pfpTexture.EncodeToJPG());
        System.IO.File.WriteAllText(jsonPath, jsonString);

        loadedMetadata.Add(metadata.tokenId, metadata);
        loadedPFP.Add(metadata.tokenId, pfpTexture);
    }

    private async Task<bool> SearchForLocalToken(string pfpPath, string jsonPath)
    {
        var localPFPRequest = UnityWebRequestTexture.GetTexture(pfpPath);
        await localPFPRequest.SendWebRequest();
        var localMetadataRequest = UnityWebRequest.Get(jsonPath);
        await localMetadataRequest.SendWebRequest();

        if (localPFPRequest.result == UnityWebRequest.Result.ProtocolError || localPFPRequest.result == UnityWebRequest.Result.ConnectionError
            || localMetadataRequest.result == UnityWebRequest.Result.ProtocolError || localMetadataRequest.result == UnityWebRequest.Result.ConnectionError)
            return false;

        print($"Found Local Oddy Data for: {jsonPath}");

        var pfpTexture = DownloadHandlerTexture.GetContent(localPFPRequest);
        var metadata = JsonConvert.DeserializeObject<OddyMetadata>(localMetadataRequest.downloadHandler.text);

        loadedMetadata.Add(metadata.tokenId, metadata);
        loadedPFP.Add(metadata.tokenId, pfpTexture);
        return true;
    }

    //public void OnGUI()
    //{
    //    if (metaMaskWallet.IsConnected)
    //    {
    //        if (GUI.Button(new Rect(10, 10, 100, 50), "Get Token URI"))
    //        {
    //            TokenURITaskHandler();
    //        }
    //    }

    //    GUI.BeginGroup(new Rect(Screen.width - 200, 10, 180, 300));
    //    GUILayout.Label($"Authorized:  {metaMaskWallet.IsAuthorized}");
    //    if (metaMaskWallet.Connecting)
    //        GUILayout.Label($"Connecting......");
    //    else
    //        GUILayout.Label($"Connected:  {metaMaskWallet.IsConnected}");
    //    GUILayout.Label($"Paused:  {metaMaskWallet.IsPaused}");
    //    GUILayout.Label($"Chain ID:  {metaMaskWallet.ChainId}");
    //    GUI.EndGroup();
    //}
}

[JsonObject]
[Serializable]
public class OddyMetadata
{
    public string image;
    public string tokenId;
    public string name;
    public OddyAttribute[] attributes;

    public override string ToString()
    {
        return $"{image}\n{tokenId}\n{name}";
    }
}


[JsonObject]
[Serializable]
public class OddyAttribute
{
    public string trait_type;
    public string value;
}