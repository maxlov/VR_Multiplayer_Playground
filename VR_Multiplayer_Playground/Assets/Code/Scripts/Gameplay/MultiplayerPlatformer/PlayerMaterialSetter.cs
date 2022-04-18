using UnityEngine;
using Photon.Pun;

public class PlayerMaterialSetter : MonoBehaviour
{
    [SerializeField] private Material[] teamColors;
    [SerializeField] private SkinnedMeshRenderer playerMesh;

    private void Start()
    {
        SetMaterial();
    }

    public void SetMaterial()
    {
        int team = 0;

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
            team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        playerMesh.material = teamColors[team];
    }
}
