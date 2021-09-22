using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    void LoadArena()
    {
        Debug.Log("LOAD ARENA");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void Start()
    {
        PhotonNetwork.Instantiate("[Character]", new Vector3(0, 1, 0), Quaternion.identity);
    }

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
}
