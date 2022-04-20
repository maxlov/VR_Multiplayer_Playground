using UnityEngine;
using Photon.Pun;

public class PlayerMaterialSetterNetwork : MonoBehaviour
{
    [SerializeField] private Material[] teamColors;
    [SerializeField] private SkinnedMeshRenderer playerMesh;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = PhotonView.Get(this);
    }

    private void Start()
    {
        SetMaterial();
    }

    public void SetMaterial()
    {
        int team = 0;

        if (!photonView.IsMine)
            return;

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
            team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        photonView.RPC("RPC_SetMaterial", RpcTarget.AllBuffered, team);
    }

    [PunRPC]
    void RPC_SetMaterial(int team)
    {
        Material[] mats = playerMesh.materials;
        mats[1] = teamColors[team];
        playerMesh.materials = mats;
    }
}
