using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class Pickupable : MonoBehaviour
{
    [SerializeField] private PickupEffect effect;
    public UnityEvent pickupEvent;
    public UnityEvent destroyEvent;

    private PhotonView photonView;

    public bool used = false;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (used)
            return;
        used = true;

        effect.Apply();
        pickupEvent.Invoke();
        photonView.RPC("RPC_DestroySelf", RpcTarget.MasterClient);
    }

    [PunRPC] void RPC_DestroySelf()
    {
        destroyEvent.Invoke();
        PhotonNetwork.Destroy(photonView);
    }
}
