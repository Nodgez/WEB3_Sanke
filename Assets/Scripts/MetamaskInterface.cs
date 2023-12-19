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

public class MetamaskInterface : MonoBehaviour
{
    private MetaMaskWallet metaMaskWallet;
    private TOD721 todContract;

    [SerializeField]
    private string contractAddress = "0x71EAa691b6e5D5E75a3ebb36D4f87CBfb23C87b0";

    void Start()
    {
        var addr = new EvmAddress(contractAddress);

        metaMaskWallet = MetaMaskUnity.Instance.Wallet;
        todContract = Contract.Attach<TOD721>(metaMaskWallet, addr);

        metaMaskWallet.WalletConnectedHandler += OnWalletConnected;
        metaMaskWallet.WalletAuthorizedHandler += OnWalletAuthorized;
        metaMaskWallet.AccountChangedHandler += OnAccountChanged;

        metaMaskWallet.Connect();

        metaMaskWallet.EthereumRequestResultReceivedHandler += OnEthResultRecieved;
    }

    private void OnEthResultRecieved(object sender, MetaMaskEthereumRequestResultEventArgs e)
    {
        // print(e.Result.ToString());
    }
    private async void OnAccountChanged(object sender, EventArgs e)
    {
        print("Wallet Account Changed:");
    }

    private void OnWalletAuthorized(object sender, EventArgs e)
    {
        print("Wallet Authorized: " + metaMaskWallet.SelectedAddress);
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void OnWalletConnected(object sender, EventArgs e)
    {
        print("Wallet Connected: " + metaMaskWallet.SelectedAddress);
    }

    /*The next step for this is to create a scriptable object Asset that will store the onchain metadata.
     * This will speed up delivery as the app won't need to contact ipfs each time the app is opened and only to get new metadata
     * I need to store the image somewhere as well
     * They should be stored in a streaming assets folder so they can be accessed in other parts of the application
    */
    private async void TokenURITaskHandler(Task<BigInteger[]> task)
    {
        await task;

        var tokenIds = task.Result;
        if (tokenIds.Length <= 1)
            return;
        for (int i = 2; i < tokenIds.Length; i++)
        {
            string pfpPath = $"{Application.persistentDataPath}/PFP/{tokenIds[i].ToString()}.jpg";
            string jsonPath = $"{Application.persistentDataPath}/PFP/{tokenIds[i].ToString()}.json";

            var localPFPRequest = UnityWebRequestTexture.GetTexture(pfpPath);
            await localPFPRequest.SendWebRequest();

            var localMetadataRequest = UnityWebRequest.Get(jsonPath);
            await localMetadataRequest.SendWebRequest();

            if (localPFPRequest.result != UnityWebRequest.Result.ProtocolError)
            {
                print($"Found Local Data");
                var localTexture = DownloadHandlerTexture.GetContent(localPFPRequest);
                var metadataText = localMetadataRequest.downloadHandler.text;
                var metadataObj = JsonConvert.DeserializeObject(metadataText);
                var q = GameObject.CreatePrimitive(PrimitiveType.Quad).GetComponent<MeshRenderer>();
                q.transform.Translate((UnityEngine.Vector3.right * i) - UnityEngine.Vector3.left * tokenIds.Length);
                q.material.SetTexture("_BaseMap", localTexture);
                continue;
            }

            var tokenURITask = todContract.TokenURI(tokenIds[i]);
            await tokenURITask;

            var url = tokenURITask.Result.Replace("ipfs://", "https://ipfs.io/ipfs/");
            var request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
            var metadata = System.Text.ASCIIEncoding.UTF8.GetString(request.downloadHandler.data);
            print(metadata);

            var oddy = JsonConvert.DeserializeObject<OddyMetadata>(metadata);
            var imgUrl = oddy.image.Replace("ipfs://", "https://ipfs.io/ipfs/");
            var pfpRequest = UnityWebRequestTexture.GetTexture(imgUrl);
            await pfpRequest.SendWebRequest();

            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad).GetComponent<MeshRenderer>();
            quad.transform.Translate((UnityEngine.Vector3.right * i) - UnityEngine.Vector3.left * tokenIds.Length);
            var texture = DownloadHandlerTexture.GetContent(pfpRequest);
            quad.material.SetTexture("_BaseMap", texture);

            if(!Directory.Exists($"{Application.persistentDataPath}/PFP"))
                Directory.CreateDirectory($"{Application.persistentDataPath}/PFP");

            File.WriteAllBytes(pfpPath, texture.EncodeToJPG());
            System.IO.File.WriteAllText(jsonPath, metadata);
        }
    }

    public void OnGUI()
    {
        if (metaMaskWallet.IsConnected)
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Get Token URI"))
            {
                TokenURITaskHandler(todContract.TokensOfOwner(new EvmAddress(metaMaskWallet.ConnectedAddress)));
            }
        }
    }
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