using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkThrowable : XRGrabInteractable
{
    [Header("Throwable")]
    public int damage;
    public HealthManager healthManager;

    public UnityEvent onHit;

    private PhotonView photonView;
    public UnityEvent onPickup;

    private bool isHot;

    private void Start()
	{
        photonView = GetComponent<PhotonView>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        photonView.RequestOwnership();
        photonView.RPC("RPC_DisableBobber", RpcTarget.All);
        onPickup.Invoke();
        onPickup.RemoveAllListeners();
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        StartCoroutine(InitializeWait());
        base.OnSelectExited(args);
    }

    IEnumerator InitializeWait()
    {
        yield return new WaitForSeconds(0.1f);
        isHot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        int layerInt = LayerMask.NameToLayer("PlayerHitBox");
        if (isHot && other.gameObject.layer == layerInt)
        {
            healthManager.RemoveHealth(damage);
            onHit.Invoke();
            photonView.RPC("RPC_DestroySelf", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void RPC_DisableBobber()
    {
        if (TryGetComponent<Bobber>(out var bobber))
            bobber.enabled = false;
    }

    [PunRPC]
    void RPC_DestroySelf()
    {
        PhotonNetwork.Destroy(photonView);
    }
}
