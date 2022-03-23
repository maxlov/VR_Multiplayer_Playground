using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.Events;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private GameObject player;

    public Transform initialSpawn;

    [Header("Tracking Transforms")]
    public Transform clientHeadTracker;
    public Transform clientLeftHandTracker;
    public Transform clientRightHandTracker;

    [SerializeField] private UnityEvent TeamJoinEvent;

    private void Awake()
    {
        if (initialSpawn == null) { initialSpawn = transform; }
    }

    #region PUN CALLBACKS
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        player = PhotonNetwork.Instantiate("Network Player 2", initialSpawn.position, Quaternion.identity);
        player.GetComponent<NetworkPlayer>().headOrigin = clientHeadTracker;
        player.GetComponent<NetworkPlayer>().leftHandOrigin = clientLeftHandTracker;
        player.GetComponent<NetworkPlayer>().rightHandOrigin = clientRightHandTracker;
        JoinTeam(0);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
    #endregion

    /// <summary>
    /// Sets local player to a team.
    /// </summary>
    /// <param name="team">Team to join. 0 = spectator, 1 = red, 2 = blue</param>
    public void JoinTeam(int team)
    {
        if (0 > team || team > 2)
        {
            Debug.Log("Out of range team, setting to spectator (0)");
            team = 0;
        }

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }
        else
        {
            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable { { "Team", team } };
            PhotonNetwork.SetPlayerCustomProperties(playerProps);
        }
        TeamJoinEvent.Invoke();
    }
}
