using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerHub : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Truck"))
        {
            Debug.Log("Stop Truck");
            TruckController truckController = other.gameObject.GetComponent<TruckController>();
            truckController.ResetTruckMode();
            truckController.gameObject.SetActive(false);
        }
    }
}
