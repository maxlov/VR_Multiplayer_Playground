using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager networkManager;

    [SerializeField] private int roomIndex;
    private PhotonView PV;

    public int currectScene;
    public int gameScene;

    [SerializeField] private UnityEvent SpawnPlayerEvent;
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
        SpawnPlayerEvent.Invoke();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room, " + newPlayer.NickName);
        base.OnPlayerEnteredRoom(newPlayer);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currectScene = scene.buildIndex;
        if (currectScene == gameScene)
        {
            Debug.Log("Starting game");
            SpawnPlayerEvent.Invoke();
            StartGameEvent.Invoke();
        }
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        Debug.Log("Loading Level");
        PhotonNetwork.LoadLevel(gameScene);
    }
}
