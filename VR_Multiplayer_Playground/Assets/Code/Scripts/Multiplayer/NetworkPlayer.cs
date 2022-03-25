using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    [Header("XR Trackable Transforms")]
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private PhotonView photonView;

    [Header("Passed in Trackers")]
    public Transform headOrigin;
    public Transform leftHandOrigin;
    public Transform rightHandOrigin;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
           return;

        MapPosition(head, headOrigin);
        MapPosition(leftHand, leftHandOrigin);
        MapPosition(rightHand, rightHandOrigin);
    }

    void MapPosition(Transform target, Transform originTransform)
    {        
        target.SetPositionAndRotation(originTransform.position, originTransform.rotation);
    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            handAnimator.SetFloat("Trigger", triggerValue);
        else
            handAnimator.SetFloat("Trigger", 0);

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            handAnimator.SetFloat("Grip", gripValue);
        else
            handAnimator.SetFloat("Grip", 0);
    }
}
