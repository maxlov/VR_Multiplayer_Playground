using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTest : MonoBehaviour
{
	public HealthManager _healthManager;
	public GameObject projectile;
	public int damage = 1;
	public float speed = 10f;
	public float shootDelay = 1f;
	public float lifetime = 3f;
	bool canShoot = true;

	void Update()
	{
		if (canShoot)
		{
			StartCoroutine(shoot());
		}

	}

	IEnumerator shoot()
	{
		Transform spawnSpoint = gameObject.transform.GetChild(1);
		GameObject bullet = Instantiate(projectile, spawnSpoint.position, spawnSpoint.rotation);
		bullet.GetComponent<TurretBullet>().damage = damage;
		bullet.GetComponent<TurretBullet>().healthManager = _healthManager;
		Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
		bulletRB.velocity = transform.TransformDirection(Vector3.forward * speed);
		Destroy(bullet, lifetime);
		canShoot = false;
		yield return new WaitForSeconds(shootDelay);
		canShoot = true;
	}
}
