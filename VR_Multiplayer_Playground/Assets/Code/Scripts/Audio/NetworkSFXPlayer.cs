using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class NetworkSFXPlayer : SFXPlayer
{
    private PhotonView photonView;

    private void Awake()
    {
        photonView = PhotonView.Get(this);
    }

    public void NetworkPlay()
    {
        photonView.RPC("RPC_NetworkPlay", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_NetworkPlay()
    {
        Play();
    }
}
