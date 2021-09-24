using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte numPlayers = 4;

    string gameVersion = "1";
    bool isConnecting = false;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
        //Cursor active
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 540, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        isConnecting = true;

        if (!PhotonNetwork.IsConnected)
        {
            // Connect Internet
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings(); //call OnConnectedToMaster
        }
        else
        {
            PhotonNetwork.JoinOrCreateRoom("SKKU", new RoomOptions { MaxPlayers = numPlayers }, null);
        }

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("connection complete!");
        
        if(isConnecting)
            PhotonNetwork.JoinOrCreateRoom("SKKU", new RoomOptions { MaxPlayers = numPlayers }, null);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.LogWarningFormat("Fail {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Create Room!");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the class ");

            PhotonNetwork.LoadLevel("BasicClassroom");
        }
        
    }
}
