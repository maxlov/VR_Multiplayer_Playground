using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform player;
    [SerializeField] private SpawnManagerSO[] spawnManagers;

    [SerializeField] private float endTime = 2f;

    [SerializeField] private UnityEvent StartGameEvent;
    [SerializeField] private UnityEvent EndGameEvent;

    public void StartGame()
    {
        Debug.Log("Starting game");
        StartGameEvent.Invoke();
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        int team = 0;

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
            team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        spawnManagers[team].AddAllSpawns();
        player.position = spawnManagers[team].GetSpawn();
    }

    public void EndGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        Debug.Log("Ending game");
        StartCoroutine(EndGameCoroutine());
    }

    IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(endTime);
        PhotonNetwork.LoadLevel(0);
    }
}
