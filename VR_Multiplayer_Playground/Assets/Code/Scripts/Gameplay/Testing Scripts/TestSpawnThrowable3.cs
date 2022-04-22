using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestSpawnThrowable3 : MonoBehaviour
{
    PhotonView photonView;
    public Transform spawnPoint;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void SpawnObject()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //    return;
        var spawned = PhotonNetwork.InstantiateRoomObject("Test3", spawnPoint.position, spawnPoint.rotation);
        //photonView.RPC("SendRefs", RpcTarget.All, spawned.GetComponent<PhotonView>().ViewID);
        //Debug.Log(spawned.GetComponent<PhotonView>().ViewID + " clientside");
    }
}
