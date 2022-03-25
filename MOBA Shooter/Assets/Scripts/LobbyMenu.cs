using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    public string roomName;

    void Start()
    {
        PhotonNetwork.CreateRoom(roomName);
        Debug.Log("Create " + roomName);
    }

    public void JoinRoom()
    {
        Debug.Log("Join " + roomName);
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }
}
