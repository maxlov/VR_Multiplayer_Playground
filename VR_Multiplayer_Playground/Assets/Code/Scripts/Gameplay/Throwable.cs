using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Throwable : MonoBehaviour
{
    public int damage;
    public HealthManager healthManager;
    [SerializeField] private PhotonView _photonView;

    public UnityEvent onHit;

    [SerializeField] private bool _isActive = false;

    private int debugStage = 0;

    private void Start()
	{
        healthManager = FindObjectOfType<HealthManager>();
    }

    public void ToggleActive(bool input)
	{
        _photonView.RPC("RPC_Active", RpcTarget.All, input);
    }
    [PunRPC]
    void RPC_Active(bool input)
    {
        Debug.Log(gameObject.name + " Acrivate: " + input.ToString() + " Stage: " + debugStage.ToString());
        debugStage += 1;
		_isActive = input;
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
                _photonView.RPC("RPC_DestroySelf", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_DestroySelf()
    {
        if (_photonView.IsMine)
            PhotonNetwork.Destroy(_photonView);
    }
}
