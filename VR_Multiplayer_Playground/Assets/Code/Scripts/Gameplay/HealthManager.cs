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

    [SerializeField] private NetworkPlayerSpawner playerSpawnerScript;

    [SerializeField] private UnityEvent playerDeath;
    [SerializeField] private UnityEvent respawnPlayer;
    [SerializeField] private UnityEvent takeDamageEvent;

    private bool _canDie = true;

    void Start()
    {
        playerSpawnerScript = FindObjectOfType<NetworkPlayerSpawner>();
        health.Value = health.defaultValue;
        ResetHealth();
    }

    public void ResetHealth()
	{
        health.Value = 100;
        playerHealthBar.value = health.defaultValue;
        _canDie = true;
        if (playerSpawnerScript.player != null)
		{
            playerSpawnerScript.player.GetComponent<NetworkPlayer>().NetworkPlayerRespawn();
        }  
    }

    public void RemoveHealth(int damage)
	{
        takeDamageEvent.Invoke();
        health.Value -= damage;
        if (health.Value > 100)
            health.Value = 100;
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
        if (playerSpawnerScript.player != null)
        {
            playerSpawnerScript.player.GetComponent<NetworkPlayer>().NetworkPlayerTakeDamage(health.Value);
        }
    }

    public void Death()
	{
        playerDeath.Invoke();
        if (playerSpawnerScript.player != null)
        {
            playerSpawnerScript.player.GetComponent<NetworkPlayer>().NetworkPlayerDeath();
        }
        StartCoroutine(RespawnTimer(respawnTime));
    }

    IEnumerator RespawnTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetHealth();
        respawnPlayer.Invoke();
    }
}
