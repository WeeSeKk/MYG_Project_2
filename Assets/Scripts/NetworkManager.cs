using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] UIManager uIManager;
    public static NetworkManager Instance;
    public GameObject playerPrefab;

    void Awake()//instance this gameobject
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Connect()//connect to photon
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Disconnect()//disconnect from photon
    {
        PhotonNetwork.Disconnect();
        Debug.Log("disconnected");
        StartCoroutine(uIManager.LogginStatusBox("Disconnected"));
    }

    public override void OnConnectedToMaster()//connect to master
    {
        base.OnConnectedToMaster();
    
        Debug.Log("Connected to Master");

        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()//Once a room is joined
    {
        Debug.Log("Joined Room");
        InstantiatePrefab();
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        StartCoroutine(uIManager.LogginStatusBox("Connected"));//start the coroutine for the box saying connected 
    }

    public static void SaveNickname(string name)//save the username of the player
    {
        PhotonNetwork.NickName = name;

        PlayerPrefs.SetString("Username", name);
    }

    void InstantiatePrefab()//Instantiate a prefab to save player information like username
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
    }
}