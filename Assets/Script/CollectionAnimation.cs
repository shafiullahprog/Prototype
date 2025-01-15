using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionAnimation : MonoBehaviour
{
    List<Transform> noofGarbages = new List<Transform>();
    public Transform targetTransform;
    public Ease easeType = Ease.OutQuad;
    public Action<Transform> onCompleteCallback;
    [SerializeField] private float spacing;

    private void Start()
    {
        foreach (Transform t in transform)
        {
            noofGarbages.Add(t);
        }
    }

    public float animationDuration = 0.5f;
    [SerializeField]int row = 0;
    [SerializeField] int column = 0;
    public void MoveObjectToTargetInSequence()
    {
        Sequence mySequence = DOTween.Sequence();
        foreach (Transform t in noofGarbages)
        {
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

    private Vector3 GetPosition(int row, int column)
    {
        Vector3 startOffset = Vector3.zero;
        if (column < 3 && row <= 5)
        {
           startOffset = new Vector3(column * spacing, 0, row * spacing);
        }
        return targetTransform.position + startOffset;
    }
}
