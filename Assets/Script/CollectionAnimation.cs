using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionAnimation : MonoBehaviour
{
    List<Transform> noofGarbages = new List<Transform>();
    public Transform truckTargetTransform, factoryTransform;
    public Ease easeType = Ease.OutQuad;
    public Action<Transform> onCompleteCallback;
    [SerializeField] private float spacing;
    public float animationDuration = 0.5f;
    int row = 0;
    int column = 0; 

    private void Start()
    {
        factoryTransform = GameObject.FindGameObjectWithTag("Factory").transform;
        foreach (Transform t in transform.GetChild(0))
        {
            noofGarbages.Add(t);
        }
    }

    public void MoveGarbageToTruck()
    {
        Sequence mySequence = DOTween.Sequence();
        foreach (Transform t in noofGarbages)
        {
            SetTruckAsParentParent(t, truckTargetTransform);
            Vector3 newPos = GetPosition(row, column);
            column++;

            if (column > 3)
            {
                column = 0;
                row++;
            }
            mySequence.Append(t.DOMove(newPos, animationDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                onCompleteCallback?.Invoke(t);
            }));
        }
    }

    public void MoveGarbageToFactory()
    {
        Sequence mySequence = DOTween.Sequence();
        foreach (Transform t in noofGarbages)
        {
            SetTruckAsParentParent(t, factoryTransform);
            mySequence.Append(t.DOMove(factoryTransform.position, animationDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                onCompleteCallback?.Invoke(t);
            }));
        }
    }

    void SetTruckAsParentParent(Transform transform,Transform target)
    {
        transform.SetParent(target);
    }

    private Vector3 GetPosition(int row, int column)
    {
        Vector3 startOffset = Vector3.zero;
        if (column < 3 && row <= 5)
        {
           startOffset = new Vector3(column * spacing, 0, -row * spacing);
        }
        return truckTargetTransform.position + startOffset;
    }
}
