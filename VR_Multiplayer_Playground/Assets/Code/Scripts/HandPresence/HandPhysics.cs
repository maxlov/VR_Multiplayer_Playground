using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody _rb;

    [SerializeField] private Renderer outOfBoundsHand;
    [SerializeField] private float showOutOfBoundsHandDistance = 0.05f;

    private Collider[] _handColliders;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _handColliders = GetComponentsInChildren<Collider>();
    }

    public void EnableHandCollider()
	{
        foreach (var item in _handColliders)
            item.enabled = true;
	}

    public void EnableHandColliderDelay(float delay)
	{
        Invoke("EnableHandCollider", delay);
	}

    public void DisableHandCollider()
    {
        foreach (var item in _handColliders)
            item.enabled = false;
    }
    private void Update()
	{
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > showOutOfBoundsHandDistance)
            outOfBoundsHand.enabled = true;
        else
            outOfBoundsHand.enabled = false;
	}

	void FixedUpdate()
    {
        _rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegrees = angleInDegrees * rotationAxis;
        _rb.angularVelocity = (rotationDifferenceInDegrees * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
