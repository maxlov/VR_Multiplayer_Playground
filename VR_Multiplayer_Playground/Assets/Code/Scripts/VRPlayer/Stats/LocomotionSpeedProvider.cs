using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class LocomotionSpeedProvider : MonoBehaviour
{
    [SerializeField] private ActionBasedContinuousMoveProvider _mover;
    [SerializeField] private FloatVariable _speed;

    private void Update()
    {
        _mover.moveSpeed = _speed.value;
    }
}
