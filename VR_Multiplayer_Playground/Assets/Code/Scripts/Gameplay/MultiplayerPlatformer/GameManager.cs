using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.Events;

public class GameManager : MonoBehaviourPunCallbacks
{
    private UnityEvent StartGameEvent;

    [SerializeField] private GameObject player;

    public void TryStartGame()
    {
        if (!photonView.IsMine)
        {
            Debug.Log("Only master client can start game");
            return;
        }

        photonView.RPC("RPC_StartGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_StartGame()
    {
        Debug.Log("Starting game");
        StartGameEvent.Invoke();
    }

}
