using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GarbageStatus : MonoBehaviour, IDataPersistence
{
    [SerializeField] Transform child;
    GameData gameData;
    public List<GameObject> garbagePresent = new List<GameObject>();
    public List<GameObject> GetTotalGarbagePresent()
    {
        return garbagePresent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Truck"))
        {
            TruckController truckController = other.GetComponent<TruckController>();
            if (truckController!=null && !truckController.IsTruckFull)
            {
                truckController.OnGarbageCollect += Collected;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Truck"))
        {
            TruckController truckController = other.GetComponent<TruckController>();
            if (truckController != null)
            {
                truckController.OnGarbageCollect -= Collected;
            }
        }
    }

    public void Collected(int index) 
    {
        GarbageController.Instance.CollectGarbage(garbagePresent[index]);

        garbagePresent.Remove(garbagePresent[index]);
    }

    public void LoadData(GameData gameData)
    {
        this.gameData = gameData;
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Save garbage data");
        //data.RemainingGarbagePositions.Clear();
        foreach (GameObject garbage in garbagePresent)
        {
            if (garbage != null && !data.RemainingGarbagePositions.ContainsKey(garbage.transform.position))
            {
                Debug.Log("Garbage parent: "+ gameObject.name);
                data.RemainingGarbagePositions.Add(garbage.transform.position, gameObject);
            }
        }
    }
}

