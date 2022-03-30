using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerParent;
    [SerializeField] private SpawnManagerSO[] spawnManagers;

    [SerializeField] private float endTime = 2f;

    [SerializeField] private UnityEvent EndGameEvent;

    public void SpawnPlayer()
    {
        int team = 0;

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
            team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        spawnManagers[team].AddAllSpawns();
        var spawn = spawnManagers[team].GetSpawn();
        if (spawn == null)
        {
            Debug.Log($"Spawn for team {team} not found. Spawning at (0,0,0)");
            spawn = Vector3.zero;
        }
        playerParent.position = spawn;
        player.position = spawn;
        Debug.Log($"Spawning player for team {team}");
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
