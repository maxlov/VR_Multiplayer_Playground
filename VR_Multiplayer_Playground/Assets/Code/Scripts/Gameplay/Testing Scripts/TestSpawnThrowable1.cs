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
        var spawned = PhotonNetwork.Instantiate("Test1", spawnPoint.position, spawnPoint.rotation);
        //photonView.RPC("SendRefs", RpcTarget.All, spawned.GetComponent<PhotonView>().ViewID);
        //Debug.Log(spawned.GetComponent<PhotonView>().ViewID + " clientside");
    }
}
