using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TruckController : MonoBehaviour
{
    public UnityAction<int> OnGarbageCollect;
    Transform wayPointsParent;
    Vector3 initialPos;
    Transform child;

    List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] string GarbageAppartmentName = "TruckStop";
    [SerializeField] string truckPathParentName = "TruckPath";
    
    [SerializeField] LayerMask vehicleLayerMask;
    
    [SerializeField] float range = 2;
    [SerializeField] private float speed = 1;
    [SerializeField] float waitTime;

    int index = 0;
    int IgarbageCollected = 0;

    public bool IsMoving = false;
    public bool IsTruckFull = false;

    public void Start()
    {
        child = transform.GetChild(1);
        initialPos = transform.position;
        wayPointsParent = GameObject.Find(truckPathParentName).transform;
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
        CheckRaycast();
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
            }
        }
    }

    private void CheckRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, vehicleLayerMask))
        {
            Debug.Log("Hit something"+ hit.transform.name);
            Debug.DrawRay(transform.position, transform.forward * range, Color.red);
            IsMoving = false;
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
                IgarbageCollected = 1;
                PlayGarbageCollectionAnimation(child, garbage);
                HaltTruck(true, false);

                OnGarbageCollect?.Invoke(0);
            }
        }
        else if(other.CompareTag("Factory") && IsTruckFull)
        {
            //PutGarbageInFacotry(other.)
            GarbageController.Instance.DeliverGarbage(IgarbageCollected);
            IgarbageCollected = 0;
            HaltTruck(false, false);
        }
    }

    void PlayGarbageCollectionAnimation(Transform targetChildObject, GarbageStatus garbageStatus)
    {
        garbageStatus.garbagePresent[0].GetComponent<CollectionAnimation>().truckTargetTransform = targetChildObject;
        garbageStatus.garbagePresent[0].GetComponent<CollectionAnimation>().MoveGarbageToTruck();
    }

    void PutGarbageInFacotry(GarbageStatus garbageStatus)
    {
        garbageStatus.garbagePresent[0].GetComponent<CollectionAnimation>().MoveGarbageToFactory();
    }
    private void HaltTruck(bool val1, bool val2)
    {
        IsTruckFull = val1;
        IsMoving = val2;
        Invoke("StartAgain", waitTime);
    }

    public void StartAgain()
    {
        Debug.Log("Start Again");
        IsMoving = true;
    }   
}
