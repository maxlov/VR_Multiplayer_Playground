using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestThrowable3 : MonoBehaviour
{
    PhotonView _photonView;
    Rigidbody _rigidBody;
    Bobber _bobber;

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidBody = GetComponent<Rigidbody>();
        _bobber = GetComponent<Bobber>();
    }

    public void TurnOnGravity()
    {
        _photonView.RPC("RPC_TurnOnGravity", RpcTarget.All);
        _photonView.RPC("TurnOffBobbler", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnOnGravity()
    {
        _rigidBody.isKinematic = false;
        _rigidBody.useGravity = true;
    }

    [PunRPC]
    void TurnOffBobbler()
	{
        _bobber.enabled = false;
    }
}
