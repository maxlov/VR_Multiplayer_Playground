using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.XR.CoreUtils;

public class NetworkPlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _clientAnimator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        XROrigin origin = FindObjectOfType<XROrigin>();
        _clientAnimator = origin.transform.parent.Find("Avatar1").GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetFloat("GripLeft", _clientAnimator.GetFloat("GripLeft"));
        _animator.SetFloat("GripRight", _clientAnimator.GetFloat("GripRight"));
        _animator.SetBool("GrabbingLeft", _clientAnimator.GetBool("GrabbingLeft"));
        _animator.SetBool("GrabbingRight", _clientAnimator.GetBool("GrabbingRight"));
    }
}
