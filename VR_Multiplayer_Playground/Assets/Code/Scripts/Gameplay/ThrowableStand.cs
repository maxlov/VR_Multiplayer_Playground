using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class ThrowableStand : MonoBehaviour
{
    public Transform spawnPoint;
    [SerializeField] private string throwableName;

    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private float respawnSpeed = 5f;

    private bool startLoading = false;

    private Throwable _throwableScript;
    private PhotonView photonView;


    void Start()
    {
        photonView = GetComponent<PhotonView>();
        StartCoroutine(InitializeWait());
    }

    IEnumerator InitializeWait()
	{
        yield return new WaitForSeconds(2);
        SpawnObject();
    }

    public void NetworkLoadBar()
    {
        photonView.RPC("LoadBar", RpcTarget.All);
    }

    [PunRPC]
    public void LoadBar()
    {
        loadingBar.value = 0;
        startLoading = true;
        loadingUI.SetActive(true);
    }

    void Update()
    {
        if (startLoading)
		{
            loadingBar.value += respawnSpeed * Time.deltaTime;
            if (loadingBar.value >= loadingBar.maxValue)
			{
                startLoading = false;
                loadingUI.SetActive(false);
                SpawnObject();
            }
        }
    }

    void SpawnObject()
	{
        if (!PhotonNetwork.IsMasterClient)
            return;
        var spawned = PhotonNetwork.Instantiate(throwableName, spawnPoint.position, spawnPoint.rotation);
        photonView.RPC("SendRefs", RpcTarget.All, spawned.GetComponent<PhotonView>().ViewID);
        Debug.Log(spawned.GetComponent<PhotonView>().ViewID + " clientside");
    }

    [PunRPC]
    public void SendRefs(int spawnedID)
	{
        Debug.Log(spawnedID + " RPC method called");
        GameObject spawned = PhotonView.Find(spawnedID).gameObject;
        if (spawned.TryGetComponent<Throwable>(out _throwableScript))
            _throwableScript.throwableStand = this;
    }
}
