using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AnimatePlayerHands : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (leftHand.TryGetFeatureValue(CommonUsages.grip, out float gripValueL))
            _animator.SetFloat("GripLeft", gripValueL);
        else
            _animator.SetFloat("GripLeft", 0);

        if (rightHand.TryGetFeatureValue(CommonUsages.grip, out float gripValueR))
            _animator.SetFloat("GripRight", gripValueR);
        else
            _animator.SetFloat("GripRight", 0);
    }

    public void SetGrabAnimationLeft(bool grabTrue)
	{
        _animator.SetBool("GrabbingLeft", grabTrue);
	}

    public void SetGrabAnimationRight(bool grabTrue)
    {
        _animator.SetBool("GrabbingRight", grabTrue);
    }
}
