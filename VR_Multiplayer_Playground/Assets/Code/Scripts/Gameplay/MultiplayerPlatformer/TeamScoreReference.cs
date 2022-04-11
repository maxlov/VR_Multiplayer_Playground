using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

[CreateAssetMenu(fileName = "TeamScoreReference", menuName = "MultiplayerVR/TeamScoreReference")]
public class TeamScoreReference : DescriptionBaseSO
{
    [SerializeField] private FloatVariable redTeamScore;
    [SerializeField] private FloatVariable blueTeamScore;
    [SerializeField] private UnityEvent updateScores;

    public float Value
    {
        get { return TeamScore().Value; }
        set { TeamScore().SetValue(value); }
    }

    private FloatVariable TeamScore()
    {
        int team = 0;

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
            team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (team < 1 || team > 2)
        {
            Debug.Log($"Tried reference team {team}, team value either spectator or not a team");
            return ScriptableObject.CreateInstance<FloatVariable>();
        }

        return team == 1 ? redTeamScore : blueTeamScore;
    }
}
