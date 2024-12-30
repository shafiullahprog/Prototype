using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GarbageStatus : MonoBehaviour
{
    [SerializeField] private List<GameObject> garbagePresent = new List<GameObject>();
    public List<GameObject> GetTotalGarbagePresent()
    {
        return garbagePresent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
        {
            garbagePresent.Add(other.gameObject);
        }

        if (other.CompareTag("Truck"))
        {
            TruckController truckController = other.GetComponent<TruckController>();

            if (truckController!=null && !truckController.IsTruckFull)
            {
                truckController.OnGarbageCollect += Collected;
                //Debug.Log(other.gameObject.name);
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
        /*for (int i = 0; i < garbagePresent.Count; i++)
        {
             GarbageController.Instance.CollectGarbage(garbagePresent[i]);
        }*/
        GarbageController.Instance.CollectGarbage(garbagePresent[index]);
        garbagePresent.Remove(garbagePresent[index]);
    }
}

