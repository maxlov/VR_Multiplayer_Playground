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
    private bool startSpawn = false;
    private float spawnValue;
    private float spawnValueGoal;

    private PhotonView photonView;


    void Start()
    {
        spawnValueGoal = loadingBar.maxValue;
        photonView = GetComponent<PhotonView>();
        StartCoroutine(InitializeWait());
    }

    IEnumerator InitializeWait()
	{
        yield return new WaitForSeconds(2);
        NetworkInstantiate();
    }

    public void NetworkLoadBar()
    {
        spawnValue = 0;
        startSpawn = true;
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
            }
        }
        if (startSpawn)
		{
            spawnValue += respawnSpeed * Time.deltaTime;
            if (spawnValue >= spawnValueGoal)
            {
                startSpawn = false;
                NetworkInstantiate();
            }
        }
    }

    public void NetworkInstantiate()
    {
        spawnPoint.GetComponent<Collider>().enabled = true;
        PhotonNetwork.Instantiate(throwable, spawnPoint.position, spawnPoint.rotation);
    }
}
