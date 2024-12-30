using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TruckController : MonoBehaviour
{
    public UnityAction<int> OnGarbageCollect;
    Transform wayPointsParent;
    Vector3 initialPos;

    [SerializeField] List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] string GarbageAppartmentName = "TruckStop";
    [SerializeField] private float speed = 1;

    int index = 0;
    [SerializeField] int IgarbageCollected = 0;

    public bool IsMoving = false;
    public bool IsTruckFull = false;

    public void Start()
    {
        initialPos = transform.position;
        wayPointsParent = GameObject.Find("TruckPath").transform;
        if (wayPointsParent != null)
        {
            foreach (Transform t in wayPointsParent)
            {
                waypoints.Add(t.gameObject);
            }
        }
    }
    private void Update()
    {
        MoveOnWayPoint();
    }
    private void MoveOnWayPoint()
    {
        if (IsMoving)
        {
            Vector3 destination = waypoints[index].transform.position;
            Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            transform.position = newPos;
            transform.LookAt(waypoints[index].transform);
            float distance = Vector3.Distance(transform.position, destination);
            if (distance <= 0.05f)
            {
                if (index < waypoints.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                //Debug.Log("Current Index: "+ index);
            }
        }
    }

    public void ResetTruckMode()
    {
        IsTruckFull = false;
        IsMoving = false;
        transform.position = initialPos;
        index = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GarbageAppartmentName) && !IsTruckFull)
        {
            GarbageStatus garbage = other.GetComponentInParent<GarbageStatus>();
            if (garbage.GetTotalGarbagePresent().Count > 0)
            {
                IgarbageCollected = garbage.GetTotalGarbagePresent().Count;
                OnGarbageCollect?.Invoke(0);
                HaltTruck(true, false);
            }
        }
        else if (other.CompareTag("Factory"))
        {
            GarbageController.Instance.DeliverGarbage(IgarbageCollected);
            IgarbageCollected = 0;
            HaltTruck(false, false);
        }
    }

    private void HaltTruck(bool val1, bool val2)
    {
        IsTruckFull = val1;
        IsMoving = val2;
        Invoke("StartAgain", 2f);
    }

    public void StartAgain()
    {
        IsMoving = true;
    }   
}
