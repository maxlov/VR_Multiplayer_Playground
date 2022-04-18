using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public int damage;
	public HealthManager healthManager;

	private void OnTriggerEnter(Collider other)
	{
		int layerInt = LayerMask.NameToLayer("PlayerHitBox");
		if (other.gameObject.layer == layerInt)
		{
			healthManager.TakeDamage(damage);
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Player"))
			Destroy(gameObject);
	}
}
