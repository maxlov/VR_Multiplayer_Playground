using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCharacterRig : MonoBehaviour
{
    [SerializeField] private float turnSmoothness = 5f;

    [SerializeField] private Transform headTarget;
    [SerializeField] private Vector3 headBodyOffset;

    [SerializeField] private VRMap head;
    [SerializeField] private VRMap leftHand;
    [SerializeField] private VRMap rightHand;


    void LateUpdate()
    {
        transform.position = headTarget.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headTarget.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}

[System.Serializable]
public class VRMap
{
    public Transform vrTarget, rigTarget;
    public Vector3 trackingPositionOffset, trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}