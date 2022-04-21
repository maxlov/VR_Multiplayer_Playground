using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Throwable : MonoBehaviour
{
    public int damage;
    public HealthManager healthManager;

    public UnityEvent onHit;

	private void Start()
	{
        healthManager = FindObjectOfType<HealthManager>();
        StartCoroutine(InitializeWait());
    }
    IEnumerator InitializeWait()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        int layerInt = LayerMask.NameToLayer("PlayerHitBox");
        if (other.gameObject.layer == layerInt)
        {
            healthManager.RemoveHealth(damage);
            onHit.Invoke();
            Destroy(gameObject);
        }
    }
}
