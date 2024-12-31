using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour, IDataPersistence
{
    [SerializeField] GarbageStatus garbageStatus;
    public GameObject garbagePrefab;
    public Transform[] spawnPoints;
    private void Start()
    {
        garbageStatus = GetComponent<GarbageStatus>();
    }

    public void SpawnGarbage()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject garbage = Instantiate(garbagePrefab, spawnPoint.position, Quaternion.identity);
            if(garbageStatus !=null)
                garbageStatus.garbagePresent.Add(garbage);
        }
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Remaining data: "+ data.RemainingGarbagePositions.Count);
        if (data.RemainingGarbagePositions.Count == 0)
        {
            Debug.Log("New garbage spawn");
            SpawnGarbage();
        }
        else
        {
            foreach(KeyValuePair<Vector3, GameObject> entry in data.RemainingGarbagePositions)
            {
                Vector3 garbagePos = entry.Key;
                GameObject garbageParent = entry.Value;

                if (garbageParent == gameObject)
                {
                    GameObject garbageToSpawn = Instantiate(garbagePrefab, garbagePos, Quaternion.identity);
                    if (garbageStatus != null)
                        garbageStatus.garbagePresent.Add(garbageToSpawn);
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
    }
}
