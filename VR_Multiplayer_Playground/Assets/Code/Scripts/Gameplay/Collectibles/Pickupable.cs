using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class Pickupable : MonoBehaviour
{
    [SerializeField] private PickupEffect effect;
    [SerializeField] private UnityEvent pickupEvent;

    private PhotonView photonView;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
    
        effect.Apply();

        pickupEvent.Invoke();
        photonView.RPC("RPC_DestroySelf", RpcTarget.MasterClient);
    }

    [PunRPC] void RPC_DestroySelf()
    {
        PhotonNetwork.Destroy(photonView);
    }
}
