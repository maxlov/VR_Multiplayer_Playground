using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.Events;
using Unity.XR.CoreUtils;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    [HideInInspector] public GameObject player;

    private Vector3 initialSpawn;

    private Transform clientHeadTracker;
    private Transform clientLeftHandTracker;
    private Transform clientRightHandTracker;

    [SerializeField] private int startTeam = 0;
    [SerializeField] private UnityEvent TeamJoinEvent;

    [SerializeField] private FloatVariable teamToJoin;

    private void Awake()
    {
        if (initialSpawn == null) { initialSpawn = transform.position; }
    }

    #region PUN CALLBACKS
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
            JoinTeam(startTeam);
    }

    public void CreatePlayer()
    {
        // There are four finds in this script, this is pretty gross but quick solution for now
        XROrigin origin = FindObjectOfType<XROrigin>();

        if (origin == null)
        {
            Debug.Log("No XROrigin found, cannot set tracking");
            return;
        }

        clientHeadTracker = origin.transform.Find("Camera Offset/Main Camera");
        clientLeftHandTracker = origin.transform.Find("Camera Offset/LeftHand Controller");
        clientRightHandTracker = origin.transform.Find("Camera Offset/RightHand Controller");

        var newPlayer = PhotonNetwork.Instantiate("Network Player 2", initialSpawn, Quaternion.identity);
        if (newPlayer.GetComponent<PhotonView>().IsMine)
            player = newPlayer;
        NetworkPlayer networkPlayer = newPlayer.GetComponent<NetworkPlayer>();
        networkPlayer.headOrigin = clientHeadTracker;
        networkPlayer.leftHandOrigin = clientLeftHandTracker;
        networkPlayer.rightHandOrigin = clientRightHandTracker;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(player);
    }
    #endregion

    /// <summary>
    /// Sets local player to a team.
    /// </summary>
    /// <param name="team">Team to join. 0 = spectator, 1 = red, 2 = blue</param>
    public void JoinTeam(int team)
    {
        Debug.Log($"Attempting to join team {team}");

        if (0 > team || team > 2)
        {
            Debug.Log("Out of range team, setting to spectator (0)");
            team = 0;
        }

        var hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash["Team"] = team;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        Debug.Log($"Player joined team {PhotonNetwork.LocalPlayer.CustomProperties["Team"]}");
        TeamJoinEvent.Invoke();
    }

    /// <summary>
    /// Sets local player to a team, using the float variable teamToJoin
    /// </summary>
    public void JoinTeam()
    {
        JoinTeam((int)teamToJoin.Value);
    }
}
