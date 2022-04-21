using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Throwable : MonoBehaviour
{
    public int damage;
    public HealthManager healthManager;

    public UnityEvent onHit;

    private PhotonView photonView;

    private void Start()
	{
        photonView = GetComponent<PhotonView>();
        healthManager = FindObjectOfType<HealthManager>();
        StartCoroutine(InitializeWait());
    }
    IEnumerator InitializeWait()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        int layerInt = LayerMask.NameToLayer("PlayerHitBox");
        if (other.gameObject.layer == layerInt)
        {
            healthManager.RemoveHealth(damage);
            onHit.Invoke();
            photonView.RPC("RPC_DestroySelf", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void RPC_DestroySelf()
    {
        PhotonNetwork.Destroy(photonView);
    }
}
