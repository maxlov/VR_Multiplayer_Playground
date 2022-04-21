using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Throwable : MonoBehaviour
{
    public int damage;
    public HealthManager healthManager;
    public ThrowableStand throwableStand;

    public UnityEvent onHit;

    private bool _isActive = false;
    private Rigidbody _rigidBody;
    private Bobber _bobber;
    private PhotonView _photonView;

    private void Start()
	{
        _photonView = GetComponent<PhotonView>();
        _rigidBody = GetComponent<Rigidbody>();
        _bobber = GetComponent<Bobber>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    public void InitialPickedUp()
	{
        if (_isActive == false)
        {
            throwableStand.NetworkLoadBar();
            _photonView.RPC("ReadyThrowable", RpcTarget.All);
            _isActive = true;
        }
    }

    [PunRPC]
    void ReadyThrowable()
	{
        _bobber.enabled = false;
        _rigidBody.isKinematic = false;
        _rigidBody.useGravity = true;
        Debug.Log("Is Ready");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive)
        {
            int layerInt = LayerMask.NameToLayer("PlayerHitBox");
            if (other.gameObject.layer == layerInt)
            {
                healthManager.RemoveHealth(damage);
                onHit.Invoke();
                _photonView.RPC("RPC_DestroySelf", RpcTarget.MasterClient);
            }
        }
    }

    [PunRPC]
    void RPC_DestroySelf()
    {
        PhotonNetwork.Destroy(_photonView);
    }
}
