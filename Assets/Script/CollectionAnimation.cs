using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionAnimation : MonoBehaviour
{
    public Transform targetTransform;
    public float animationDuration = 1.5f;
    public Ease easeType = Ease.OutQuad;
    public System.Action onCompleteCallback;

    public void MoveObjectToTarget(Transform objectTransform)
    {
        if (targetTransform == null)
        {
            Debug.LogWarning("Target Transform is not set.");
            return;
        }

        // Animate the object's position to the target's position
        objectTransform.DOMove(targetTransform.position, animationDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                // Callback on completion
                onCompleteCallback?.Invoke();
            });
    }

    // Optional: Set the target dynamically
    public void SetTarget(Transform newTarget)
    {
        targetTransform = newTarget;
    }
}
