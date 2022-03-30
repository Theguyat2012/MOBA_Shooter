using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    private string roomName = "room";

    public void Start()
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void CreateRoom()
    {
        // PhotonNetwork.CreateRoom(roomName, new RoomOptions(){ MaxPlayers = 12});
        // PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }
}
