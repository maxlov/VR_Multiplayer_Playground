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

    private bool _isActive = false;
    private PhotonView _photonView;

    private void Start()
	{
        _photonView = GetComponent<PhotonView>();
        healthManager = FindObjectOfType<HealthManager>();
        StartCoroutine(InitializeWait());
    }

    IEnumerator InitializeWait()
    {
        yield return new WaitForSeconds(0.1f);
        ToggleCollider(true);
    }

    public void ToggleCollider(bool input)
	{
        _photonView.RPC("RPC_ToggleCollider", RpcTarget.All, input);
    }
    [PunRPC]
    void RPC_ToggleCollider(bool input)
    {
        gameObject.GetComponent<Collider>().enabled = input;
    }

    public void Activate ()
	{
        _photonView.RPC("RPC_Active", RpcTarget.All);
    }
    [PunRPC]
    void RPC_Active()
    {
        _isActive = true;
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
