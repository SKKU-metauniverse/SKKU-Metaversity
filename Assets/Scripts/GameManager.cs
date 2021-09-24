using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    #region Private
    [SerializeField]
    private GameObject escapePN;

    #endregion

    #region Public
    public GameObject spawn;
    public Camera C1;
    #endregion

    private void Start()
    {
        PhotonNetwork.Instantiate("[Character]", spawn.transform.position, Quaternion.identity);
        C1.enabled = false;

        escapePN.SetActive(false);
        
        // Cursor lock
        Cursor.lockState = CursorLockMode.Locked;

        // Cursor visible
        Cursor.visible = false;


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

    public void downPanel()
    {
        // Cursor lock
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor visible
        Cursor.visible = false;
        escapePN.SetActive(false);
    }

    

    
}
