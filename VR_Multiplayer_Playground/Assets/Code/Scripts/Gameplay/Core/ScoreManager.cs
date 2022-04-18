using UnityEngine;
using Photon.Pun;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private FloatVariable redScore;
    [SerializeField] private FloatVariable blueScore;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = PhotonView.Get(this);
    }

    public void SendScores()
    {
        Debug.Log("Sending Score Update");
        photonView.RPC("RPC_UpdateScores", RpcTarget.OthersBuffered, redScore.Value, blueScore.Value);
    }

    [PunRPC]
    void RPC_UpdateScores(float red, float blue)
    {
        redScore.SetValue(red);
        blueScore.SetValue(blue);
    }
}
