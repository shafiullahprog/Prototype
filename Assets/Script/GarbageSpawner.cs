using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour, IDataPersistence
{
    public GameObject garbagePrefab;
    public Transform[] spawnPoints;

    private List<GameObject> activeGarbage = new List<GameObject>();

   
    public void SpawnGarbage()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject garbage = Instantiate(garbagePrefab, spawnPoint.position, Quaternion.identity);
            activeGarbage.Add(garbage);
        }
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Remaining data: "+ data.RemainingGarbagePositions.Count);
        foreach (GameObject garbage in activeGarbage)
        {
            if (garbage != null)
            {
                Destroy(garbage);
            }
        }
        activeGarbage.Clear();
        if (data.RemainingGarbagePositions.Count == 0)
        {
            SpawnGarbage();
        }
        else
        {
            // Load garbage positions from saved data
            foreach (Vector3 position in data.RemainingGarbagePositions)
            {
                GameObject garbage = Instantiate(garbagePrefab, position, Quaternion.identity);
                activeGarbage.Add(garbage);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
    }
}
