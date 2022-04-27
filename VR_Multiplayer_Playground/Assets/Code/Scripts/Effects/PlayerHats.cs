using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class PlayerHats : MonoBehaviour
{
	[SerializeField] private Transform hats;
	[SerializeField] private UnityEvent hatChangeEvent;

	public int HatIndex;

	public void ChangeHats()
	{
		HatIndex = Random.Range(0, hats.childCount);
		for (int i = 0; i < hats.childCount; i++)
		{
			if (i == HatIndex)
				hats.GetChild(i).gameObject.SetActive(true);
			else
				hats.GetChild(i).gameObject.SetActive(false);
		}
		var hash = PhotonNetwork.LocalPlayer.CustomProperties;
		hash["Hat"] = HatIndex;
		PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		hatChangeEvent.Invoke();
	}
}
