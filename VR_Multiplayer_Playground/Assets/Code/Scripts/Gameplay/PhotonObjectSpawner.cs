using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonObjectSpawner : MonoBehaviour
{
    [SerializeField] string prefabName;
    private GameObject lastSpawned;

    [SerializeField] private Transform spawnPoint;

    public void SpawnObject()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        lastSpawned = PhotonNetwork.Instantiate(prefabName, spawnPoint.position, Quaternion.identity);
    }
}
