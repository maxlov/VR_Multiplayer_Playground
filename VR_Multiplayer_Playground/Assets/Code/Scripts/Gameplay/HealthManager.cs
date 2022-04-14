using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private FloatVariable health;
    [SerializeField] private Slider playerHealthBar;

    [SerializeField] private float respawnTime;

    // need refrence to network player's health bar

    [SerializeField] private UnityEvent playerDeath;
    [SerializeField] private UnityEvent respawnPlayer;

    private bool _canDie = true;

    void Start()
    {
        health.Value = health.defaultValue;
        ResetHealth();
    }

    public void ResetHealth()
	{
        health.Value = 100;
        playerHealthBar.value = health.defaultValue;
        // send update to network player healthbar display
        _canDie = true;
    }

    public void TakeDamage(int damage)
	{
        health.Value -= damage;
        if (health.Value <= 0)
		{
            health.Value = 0;
            if (_canDie)
			{
                Death();
                _canDie = false;
            }
		}
        playerHealthBar.value = health.Value;
        // send update to network player healthbar display
    }

    public void Death()
	{
        playerDeath.Invoke();
        // make the network player disapear with the particles and sounds
        StartCoroutine(RespawnTimer(respawnTime));
    }

    IEnumerator RespawnTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetHealth();
        respawnPlayer.Invoke();
    }
}
