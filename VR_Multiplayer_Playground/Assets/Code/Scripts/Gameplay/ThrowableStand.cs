using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class ThrowableStand : MonoBehaviour
{
    [SerializeField] private string throwable;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private float respawnSpeed = 5f;

    private bool startLoading = false;

    private PhotonView photonView;


    void Start()
    {
        photonView = GetComponent<PhotonView>();
        StartCoroutine(InitializeWait());
    }

    IEnumerator InitializeWait()
	{
        yield return new WaitForSeconds(2);
        SpwanObject();
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
        spawnPoint.gameObject.SetActive(false);
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
                photonView.RPC("EnableSocket", RpcTarget.All);
                SpwanObject();
            }
        }
    }

    [PunRPC]
    public void EnableSocket()
    {
        spawnPoint.gameObject.SetActive(true);
    }

    void SpwanObject()
	{
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.Instantiate(throwable, spawnPoint.position, spawnPoint.rotation);
    }
}
