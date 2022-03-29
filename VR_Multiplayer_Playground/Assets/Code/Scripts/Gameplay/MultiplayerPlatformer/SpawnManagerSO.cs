using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnManager", menuName = "MultiplayerVR/SpawnManager")]
public class SpawnManagerSO : DescriptionBaseSO
{
    public string spawnTag;

    [System.NonSerialized] public List<Transform> teamSpawns = new List<Transform>();

    public Vector3 GetSpawn()
    {
        Debug.Log(spawnTag + " " + teamSpawns.Count);
        return teamSpawns[Random.Range(0, teamSpawns.Count)].position;
    }

    public void AddAllSpawns()
    {
        teamSpawns.Clear();

        var spawns = GameObject.FindGameObjectsWithTag(spawnTag);
        foreach (var spawn in spawns)
            teamSpawns.Add(spawn.transform);
    }
}
