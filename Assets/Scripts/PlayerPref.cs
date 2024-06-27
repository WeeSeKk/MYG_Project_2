using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPref : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
    UIManager uIManager;
    void Awake()
    {
        if (photonView.IsMine)//if this prefab is mine
        {
            PlayerPref.LocalPlayerInstance = this.gameObject;
            uIManager = GameObject.Find("UIDocument").GetComponent<UIManager>();
            uIManager.SetupList(this.photonView.Owner.NickName);//call the fonction adding the nickname in the list
        }
        
        DontDestroyOnLoad(this.gameObject);   
    }
}