using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestThrowable2 : MonoBehaviour
{
    PhotonView _photonView;
    Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void TurnOnGravity()
    {
        _photonView.RPC("RPC_TurnOnGravity", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnOnGravity()
    {
        _rigidBody.isKinematic = false;
        _rigidBody.useGravity = true;
    }
}
