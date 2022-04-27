using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager networkManager;

    [SerializeField] private int roomIndex;
    private PhotonView PV;

    private int currectScene;
    public int lobbyScene;
    public FloatReference targetScene;

    [SerializeField] private UnityEvent NetworkSpawnPlayerEvent;
    [SerializeField] private UnityEvent StartGameEvent;

    private void Awake()
    {
        if (NetworkManager.networkManager != null)
        {
            Destroy(gameObject);
            return;
        }
        networkManager = this;
        DontDestroyOnLoad(this.gameObject);

        PhotonNetwork.AutomaticallySyncScene = true;
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        Debug.Log("Try Connect to server");
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server.");
        base.OnConnected();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom("Room " + roomIndex.ToString(), roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
        NetworkSpawnPlayerEvent.Invoke();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room, " + newPlayer.NickName);
        base.OnPlayerEnteredRoom(newPlayer);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currectScene = scene.buildIndex;

        if (currectScene != lobbyScene)
        {
            Debug.Log("Starting game");
            StartGameEvent.Invoke();
        }

        if (PhotonNetwork.InRoom)
            NetworkSpawnPlayerEvent.Invoke();
    }

    public void LoadLevel()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        StartCoroutine(LoadLevelCoroutine());
    }

    IEnumerator LoadLevelCoroutine()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Loading " + SceneManager.GetSceneByBuildIndex((int)targetScene.Value).name);
        PhotonNetwork.LoadLevel((int)targetScene.Value);
    }
    
}
