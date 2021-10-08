using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte numPlayers = 4;
    [SerializeField]
    private InputField inputField;

    string gameVersion = "1";
    bool isConnecting = false;
    bool isCreate = false;

    string roomName;

    private static System.Random random = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF); //랜덤 시드값

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

    public static string RandomString(int _nLength = 12)
    {
        const string strPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  //문자 생성 풀
        char[] chRandom = new char[_nLength];

        for (int i = 0; i < _nLength; i++)
        {
            chRandom[i] = strPool[random.Next(strPool.Length)];
        }
        string strRet = new String(chRandom);   // char to string
        return strRet;
    }
    public void Create()
    {
        isConnecting = true;
        isCreate = true;

        if (!PhotonNetwork.IsConnected)
        {
            // Connect Internet
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings(); //call OnConnectedToMaster
        }
        else
        {
            roomName = RandomString();
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = numPlayers }, null);
        }

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
            string inputName = inputField.text;
            PhotonNetwork.JoinRoom(inputName);
        }

    }

    public override void OnConnectedToMaster()
    { 
        base.OnConnectedToMaster();
        Debug.Log("connection complete!");
        
        if(isConnecting && isCreate)
        {
            roomName = RandomString();
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = numPlayers }, null);
        }
        else if(isConnecting && !isCreate)
        {
            string inputName = inputField.text;
            PhotonNetwork.JoinRoom(inputName);
        }
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
        Debug.Log(roomName);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the class ");

            PhotonNetwork.LoadLevel("NatureClassroom");
        }
        
    }
}
