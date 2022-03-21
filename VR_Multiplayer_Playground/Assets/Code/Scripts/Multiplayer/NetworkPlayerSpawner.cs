using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

    public Transform clientHeadTracker;
    public Transform clientLeftHandTracker;
    public Transform clientRightHandTracker;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player 2", transform.position, transform.rotation);
        spawnedPlayerPrefab.GetComponent<NetworkPlayer>().headOrigin = clientHeadTracker;
        spawnedPlayerPrefab.GetComponent<NetworkPlayer>().leftHandOrigin = clientLeftHandTracker;
        spawnedPlayerPrefab.GetComponent<NetworkPlayer>().rightHandOrigin = clientRightHandTracker;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
