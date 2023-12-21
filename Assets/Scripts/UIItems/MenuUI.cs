using MetaMask.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : UIItem
{
    [SerializeField] private Button connectionButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button refreshNFTsButton;

    private void Start()
    {
        Open();
    }

    protected override void Close()
    {
        var wallet = MetaMaskUnity.Instance.Wallet;
        wallet.WalletReadyHandler -= OnWalletReady;
        connectionButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        refreshNFTsButton.gameObject.SetActive(false);
    }

    protected override void Open()
    {
        var wallet = MetaMaskUnity.Instance.Wallet;
        wallet.WalletReadyHandler += OnWalletReady;
        connectionButton.gameObject.SetActive(!wallet.IsConnected);
        playButton.gameObject.SetActive(wallet.IsConnected && wallet.IsAuthorized);          
        refreshNFTsButton.gameObject.SetActive(wallet.IsConnected && wallet.IsAuthorized);
    }

    void OnWalletReady(object sender, EventArgs e)
    {
        connectionButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        refreshNFTsButton.gameObject.SetActive(true);
    }

    public void ConnectMetaMaskWallet()
    {
        MetaMaskUnity.Instance.Connect();
    }

    public void Play()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene(1);
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
