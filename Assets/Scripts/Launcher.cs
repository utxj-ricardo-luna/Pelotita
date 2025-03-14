using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();        
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al server master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name,spawnPoint.position,spawnPoint.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
