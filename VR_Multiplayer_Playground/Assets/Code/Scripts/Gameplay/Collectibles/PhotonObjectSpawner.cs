using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonObjectSpawner : MonoBehaviour
{
    [SerializeField] string prefabName;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private bool spawnOnStart;
    [SerializeField] private bool oneShot;

    private GameObject lastSpawned;

    private void Start()
    {
        if (spawnOnStart)
            SpawnObject();
    }

    private void Update()
    {
        // Having a check every update is far from ideal but without this
        // it is difficult to tell when the last object is destroyed.
        // Perhaps I can add on some script to the object that raises some sort of event?

        if (oneShot && lastSpawned == null)
            SpawnObject();
    }

    public void SpawnObject()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        lastSpawned = PhotonNetwork.Instantiate(prefabName, spawnPoint.position, Quaternion.identity);
    }
}
