using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private FloatVariable score;
    [SerializeField] private UnityEvent ScoreUpdateEvent;
    [SerializeField] Transform parent;

    public void Spawn()
    {
        int spawnCost = 5;

        if (spawnCost > score.Value)
            return;

        //Score Stuff
        score.SetValue(score.Value -= spawnCost);
        Debug.Log($"{score.Value}");
        ScoreUpdateEvent.Invoke();

        GameObject.Instantiate(spawnedObject, parent.position, Quaternion.identity);
        Debug.Log("Spawned Object");
    }
}
