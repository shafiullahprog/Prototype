using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    public GameObject garbagePrefab;
    public Transform[] spawnPoints;
    public void Start()
    {
        SpawnGarbage();
    }

    public void SpawnGarbage()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(garbagePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
