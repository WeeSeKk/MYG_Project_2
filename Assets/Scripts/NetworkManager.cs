using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;
using UnityEngine.UIElements;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] UIManager uIManager;
    public static NetworkManager Instance;

    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Disconnect()
    {
        uIManager.loggedCount.Clear();
        PhotonNetwork.Disconnect();
        Debug.Log("disconnected");
        StartCoroutine(uIManager.LogginStatusBox("Disconnected"));
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
    
        Debug.Log("Connected to Master");

        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        Debug.Log(PhotonNetwork.CurrentRoom.Name);

        StartCoroutine(uIManager.LogginStatusBox("Connected"));
    }
}