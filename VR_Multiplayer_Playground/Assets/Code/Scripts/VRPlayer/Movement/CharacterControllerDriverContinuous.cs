using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterControllerDriverContinuous : CharacterControllerDriver
{
    private CapsuleCollider _collider;

    private void LateUpdate()
    {
        UpdateCharacterController();
    }

    private void OnBeginLocomotion(LocomotionSystem system)
    {
    }

    private void OnEndLocomotion(LocomotionSystem system)
    {
    }
}
