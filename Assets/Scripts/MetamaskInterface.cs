using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using MetaMask;
using MetaMask.Unity;
using MetaMask.Models;
using Newtonsoft.Json;
using System.Linq;
using evm.net.Models;
using evm.net;
using Contracts;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class MetamaskInterface : MonoBehaviour
{
    private MetaMaskWallet metaMaskWallet;

    private string randomTokenId;
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

    private async void TokenURITaskHandler(Task<string> task)
    {
        await task;
        print(task.Result);

        var url = task.Result.Replace("ipfs://", "https://ipfs.io/ipfs/");
        var request = UnityWebRequest.Get(url);
        await request.SendWebRequest();
        var metadata = System.Text.ASCIIEncoding.UTF8.GetString(request.downloadHandler.data);
        print(metadata);

        var oddy = JsonConvert.DeserializeObject<OddyMetadata>(metadata);
        var imgUrl = oddy.image.Replace("ipfs://", "https://ipfs.io/ipfs/");
        var pfpRequest = UnityWebRequestTexture.GetTexture(imgUrl);
        await pfpRequest.SendWebRequest();

        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad).GetComponent<MeshRenderer>();
        var texture = DownloadHandlerTexture.GetContent(pfpRequest);
        quad.material.SetTexture("_MainTex", texture);
    }

    public void OnGUI()
    {
        if (metaMaskWallet.IsConnected)
        {
            if (GUI.Button(new Rect(10, 10, 300, 300), "Get Token URI"))
            {
                TokenURITaskHandler(todContract.TokenURI(new BigInteger(3067)));
            }
        }

    }
}

[JsonObject]
public class OddyMetadata
{
    public string image;
    public string tokenId;
    public string name;
    public OddyAttribute[] attributes;
}


[JsonObject]
public class OddyAttribute
{
    public string trait_type;
    public string value;
}