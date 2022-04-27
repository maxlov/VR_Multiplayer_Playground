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

    private bool _startLoading = false;
    private bool _canLoad = true;
    private GameObject _throwable;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable") && _canLoad)
            if (_throwable == null)
                _throwable = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == _throwable)
		{
            NetworkLoadBar();
        }
	}

	public void NetworkLoadBar()
    {
        if (_canLoad)
            photonView.RPC("LoadBar", RpcTarget.All);
    }

    [PunRPC]
    public void LoadBar()
    {
        loadingBar.value = 0;
        _startLoading = true;
        loadingUI.SetActive(true);
        _canLoad = false;
    }

    void Update()
    {
        if (_startLoading)
		{
            loadingBar.value += respawnSpeed * Time.deltaTime;
            if (loadingBar.value >= loadingBar.maxValue)
			{
                _startLoading = false;
                loadingUI.SetActive(false);
                _throwable = null;
                _canLoad = true;
                SpawnObject();
            }
        }
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
