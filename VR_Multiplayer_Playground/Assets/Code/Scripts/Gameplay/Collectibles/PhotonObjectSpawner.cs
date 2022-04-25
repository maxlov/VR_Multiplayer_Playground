using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhotonObjectSpawner : MonoBehaviour
{
    [SerializeField] string prefabName;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool spawnOnStart;
    [SerializeField] private UnityEvent onPickedUp;

    private GameObject lastSpawned;
    private Pickupable pickupable;

    private void Start()
    {
        if (spawnOnStart)
            SpawnObject();
    }

    public void SpawnObject()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        lastSpawned = PhotonNetwork.Instantiate(prefabName, spawnPoint.position, Quaternion.identity);
        if (lastSpawned.TryGetComponent<Pickupable>(out pickupable))
            pickupable.destroyEvent.AddListener(OnObjectPickup);
    }

    private void OnObjectPickup()
    {
        pickupable.pickupEvent.RemoveListener(OnObjectPickup);
        onPickedUp.Invoke();
    }
}
