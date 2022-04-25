using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHats : MonoBehaviour
{
	[SerializeField] private Transform hats;
	[SerializeField] private NetworkPlayerSpawner networkPlayer;

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
		if (networkPlayer.player)
			networkPlayer.player.GetComponent<NetworkPlayer>().ChooseHat(HatIndex);
	}
}
