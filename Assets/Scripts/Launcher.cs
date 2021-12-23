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
    public GameObject roomTypePN;
    public GameObject joinRoomPN;
    public InputField roomCodeInput;
    public InputField nickNameInput;
    public Dropdown maxPlayerDropDown;
    public Dropdown characterDropDown;
    public Warning warningSys;

    string gameVersion = "1";
    string roomName;
    string roomType;
    string characterType = "[Woman]";
    List<RoomInfo> roomList;

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

        CheckAndReadyConncection();
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

    public void TurnOnRoomTypePanel() => roomTypePN.SetActive(true);
    public void TurnOffRoomTypePanel() => roomTypePN.SetActive(false);
    public void TurnOnJoinRoomPanel() => joinRoomPN.SetActive(true);
    public void TurnOffJoinRoomPanel() => joinRoomPN.SetActive(false);

    public void SetCharacterType()
    {
        characterDropDown.Hide();
        characterType = characterDropDown.options[characterDropDown.value].text;
    }

    public void JoinRoom()
    {
        if (!ValidateNickName()) return;

        if (PhotonNetwork.IsConnected)
        {
            Hashtable characterOptions = new Hashtable() { { "CharacterType", characterType } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(characterOptions);
            PhotonNetwork.LocalPlayer.NickName = nickNameInput.text;

            roomName = roomCodeInput.text;
            if (!ValidateRoomCode()) return;

            PhotonNetwork.JoinRoom(roomName);
        }
        else CheckAndReadyConncection();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1) PhotonNetwork.LoadLevel(roomType);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        warningSys.PopWarningMsg(message);
    }


    public void SetRoomTypeAndCreate(string _roomType)
    {
        roomType = _roomType;

        if (!ValidateNickName()) return;

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Create Room");
            roomName = RandomString();

            Hashtable characterOptions = new Hashtable() { { "CharacterType", characterType } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(characterOptions);
            PhotonNetwork.LocalPlayer.NickName = nickNameInput.text;

            RoomOptions roomOptions = new RoomOptions();

            Debug.Log("Setting max player Room");
            print(maxPlayerDropDown.options[maxPlayerDropDown.value].text);
            print(Convert.ToByte(maxPlayerDropDown.value + 1));
            roomOptions.MaxPlayers = Convert.ToByte(maxPlayerDropDown.value + 1);

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
        else
        {
            CheckAndReadyConncection();
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        warningSys.PopWarningMsg(message);
    }
    private void CheckAndReadyConncection()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Conncection Is Failed");
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings(); //call OnConnectedToMaster
        }
    }

    private bool ValidateNickName()
    {
        if (nickNameInput.text.Trim().Length == 0)
        {
            roomTypePN.SetActive(false);
            joinRoomPN.SetActive(false);
            warningSys.PopWarningMsg("Check Your Nickname!");
            return false;
        }

        return true;
    }

    private bool ValidateRoomCode()
    {
        if (roomName.Trim().Length == 0)
        {
            joinRoomPN.SetActive(false);
            warningSys.PopWarningMsg("Check Your RoomCode!");
            return false;
        }

        return true;
    }

    

}