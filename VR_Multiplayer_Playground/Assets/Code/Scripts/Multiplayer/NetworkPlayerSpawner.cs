using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.Events;
using Unity.XR.CoreUtils;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject player;

    public Vector3 initialSpawn;

    private Transform clientHeadTracker;
    private Transform clientLeftHandTracker;
    private Transform clientRightHandTracker;

    [SerializeField] private UnityEvent TeamJoinEvent;

    private void Awake()
    {
        if (initialSpawn == null) { initialSpawn = transform.position; }
    }

    #region PUN CALLBACKS
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        JoinTeam(0);
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

        player = PhotonNetwork.Instantiate("Network Player 2", initialSpawn, Quaternion.identity);
        NetworkPlayer networkPlayer = player.GetComponent<NetworkPlayer>();
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
