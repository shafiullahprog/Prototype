using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageAnimation : MonoBehaviour
{
    [SerializeField] List<Transform> gameObjects = new List<Transform>();

    private void Start()
    {
       foreach(Transform t in transform)
        {
            gameObjects.Add(t);
        }
    }
}
