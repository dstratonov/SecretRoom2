using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class animationPositionAdjuster : MonoBehaviour
{
    public Transform targetTransform;
    public Transform casterTransform;

    public Vector3 animationOffset;
    void Start()
    {
        casterTransform.rotation = buildRotation(targetTransform);
        casterTransform.position = buildPosition(targetTransform);
    }



    private Quaternion buildRotation(Transform targetTransform)
    {
        Vector3 upVector = targetTransform.up;
        Vector3 forwardVector = -targetTransform.forward;
        return Quaternion.LookRotation(forwardVector, upVector);
    }

    private Vector3 buildPosition(Transform targetTransform)
    {
        Vector3 upVector = targetTransform.up;
        Vector3 forwardVector = targetTransform.forward;
        Vector3 rightVector = targetTransform.right;

        return rightVector * animationOffset.x + upVector * animationOffset.y + forwardVector * animationOffset.z;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
