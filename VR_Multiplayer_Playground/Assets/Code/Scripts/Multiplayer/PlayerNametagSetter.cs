using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNametagSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nametag;

    public void Start()
    {
        UpdateNametag();
    }

    public void UpdateNametag()
    {
        var photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
            return;
        photonView.RPC("RPC_UpdateNametag", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void RPC_UpdateNametag()
    {
        nametag.text = PhotonNetwork.LocalPlayer.NickName;
    }

}
