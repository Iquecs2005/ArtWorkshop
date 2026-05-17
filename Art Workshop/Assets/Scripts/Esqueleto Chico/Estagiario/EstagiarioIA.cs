using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EstagiarioIA : MonoBehaviour
{
    [SerializeField] private Transform followTransform;

    [SerializeField] private UnityEvent<Vector2> OnGoalChange;
    [SerializeField] private UnityEvent<Vector2> OnShootTargetChange;

    private void FixedUpdate()
    {
        Vector2 dirVector = followTransform.position - transform.position;
        dirVector.Normalize();
        OnGoalChange.Invoke(dirVector);
        OnShootTargetChange.Invoke(dirVector);
    }
}
