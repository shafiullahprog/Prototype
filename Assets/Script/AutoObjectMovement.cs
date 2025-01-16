using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoObjectMovement : MonoBehaviour
{
    List<GameObject> waypoints = new List<GameObject>();
    Transform wayPointsParent;

    [SerializeField] string truckPathParentName = "TruckPath";

    public bool IsMoving = true;

    int index = 0;
    [SerializeField] float speed = 1;

    private void Start()
    {
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
}
