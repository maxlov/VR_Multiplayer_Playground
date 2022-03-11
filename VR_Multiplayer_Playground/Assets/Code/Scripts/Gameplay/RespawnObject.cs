using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
	private Vector3 spawnPosition;
	private Quaternion spawnRotation;

	private void Start()
	{
		spawnPosition = transform.position;
		spawnRotation = transform.rotation;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Respawn"))
		{
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = spawnPosition;
			transform.rotation = spawnRotation;
		}
	}
}
