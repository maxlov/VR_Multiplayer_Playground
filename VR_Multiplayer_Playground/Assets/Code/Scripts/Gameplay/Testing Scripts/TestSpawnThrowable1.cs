using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestSpawnThrowable1 : MonoBehaviour
{
    PhotonView photonView;
    public Transform spawnPoint;

	void Start()
	{
		
	}

	public void SpawnObject()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //    return;
        var spawned = PhotonNetwork.InstantiateRoomObject("Test1", spawnPoint.position, spawnPoint.rotation);
    }
}
