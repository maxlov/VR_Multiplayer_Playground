using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class ThrowableStand : MonoBehaviour
{
    [SerializeField] private string throwableName;
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
        SpawnObject();
    }

	//private void OnTriggerExit(Collider other)
	//{
 //       if (other.CompareTag("Throwable"))
 //           NetworkLoadBar();
 //   }

	public void NetworkLoadBar()
    {
        photonView.RPC("LoadBar", RpcTarget.All);
        photonView.RPC("ToggleSocket", RpcTarget.All, false);
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
                photonView.RPC("ToggleSocket", RpcTarget.All, true);
                SpawnObject();
            }
        }
    }

    [PunRPC]
    void ToggleSocket(bool toggle)
	{
        spawnPoint.gameObject.SetActive(toggle);
	}

    void SpawnObject()
	{
        if (!PhotonNetwork.IsMasterClient)
            return;
        var spawned = PhotonNetwork.Instantiate(throwableName, spawnPoint.position, spawnPoint.rotation);
        //if (spawned.TryGetComponent<Throwable>(out _throwableScript))
        //    _throwableScript.throwableStand = this;
    }
}
