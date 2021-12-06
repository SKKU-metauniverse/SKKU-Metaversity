using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte numPlayers = 4;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private GameObject roomTypePN;
    [SerializeField]
    private Dropdown dropDown;

    string gameVersion = "1";

    string roomName;
    string roomType;
    string characterType = "[Woman]";
    bool isCreate;

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
        roomTypePN.SetActive(false);

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            // Connect Internet
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings(); //call OnConnectedToMaster

        }
    }

    // Update is called once per frame
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
    public void TurnOnRoomTypePanel()
    {
        roomTypePN.SetActive(true);

    }

    public void SetCharacterType()
    {
        dropDown.Hide();
        characterType = dropDown.options[dropDown.value].text;
        //Debug.Log(string.Format("Set Character Type {0}", characterType));
    }

    public void Connect(bool _isCreate)
    {
        isCreate = _isCreate;


        //roomOptions.CustomRoomProperties = new Hashtable() { { "CharacterType", characterType } };
        //Debug.Log(string.Format("Create or Join Room! {0}, {1}", roomName, PhotonNetwork.LocalPlayer.ToStringFull()));

        if (PhotonNetwork.IsConnected)
        {
            roomName = (isCreate) ? RandomString() : inputField.text;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = numPlayers;

            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "CharacterType", characterType } });
            //Debug.Log(string.Format("Create or Join Room! {0}, {1}", roomName, PhotonNetwork.LocalPlayer.ToStringFull()));

            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.LogWarningFormat("OnDisconnected : {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        PhotonNetwork.LoadLevel(roomType);


        
    }


    public void SetRoomTypeAndCreate(string _roomType)
    {
        roomType = _roomType;
        Connect(true);
    }
}
