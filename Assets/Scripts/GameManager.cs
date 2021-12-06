using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{

    #region Private
    [SerializeField]
    private GameObject escapePN;
    [SerializeField]
    private Text code;

    #endregion

    #region Public
    public GameObject spawn;
    public Camera C1;
    public GameObject fileScanner;
    #endregion

    private void Start()
    {
        Hashtable CP = PhotonNetwork.LocalPlayer.CustomProperties; //CurrentRoom.CustomProperties;
        Debug.Log(string.Format("Character Initiate {0} {1}", CP["CharacterType"], PhotonNetwork.LocalPlayer.NickName));
        PhotonNetwork.Instantiate((string)CP["CharacterType"], spawn.transform.position, Quaternion.identity);
        C1.enabled = false;

        escapePN.SetActive(false);
        
        // Cursor lock
        Cursor.lockState = CursorLockMode.Locked;

        // Cursor visible
        Cursor.visible = false;

        code.text = PhotonNetwork.CurrentRoom.Name.ToString();
    }


    #region PunCallbacks

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("OnLeftRoom : BYE~~~");
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //LoadArena();
        Debug.Log("OnPlayerEnteredRoom : Enter!!!");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        //LoadArena();
        Debug.Log("OnPlayerLeftRoom : Someone BYE~~~");
    }

    #endregion


    void LoadArena()
    {
        Debug.Log("LOAD ARENA");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /*public void downPanel()
    {
        // Cursor lock
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor visible
        Cursor.visible = false;
        escapePN.SetActive(false);
    }*/


    public void downPanel()
    {
        if (!fileScanner.activeSelf)
        {
            // Cursor lock
            Cursor.lockState = CursorLockMode.Locked;
            // Cursor visible
            Cursor.visible = false;
        }

        escapePN.SetActive(false);
    }

}
