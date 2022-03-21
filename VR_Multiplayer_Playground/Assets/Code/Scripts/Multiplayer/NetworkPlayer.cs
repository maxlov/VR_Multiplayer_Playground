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

    //[Header("Hand Animators")]
    //public Animator leftHandAnimator;
    //public Animator rightHandAnimator;

    private PhotonView photonView;

    [Header("Passed in Trackers")]
    public Transform headOrigin;
    public Transform leftHandOrigin;
    public Transform rightHandOrigin;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine)
            return;

        //headOrigin = origin.transform.Find("Camera Offset/Main Camera");
        //leftHandOrigin = origin.transform.Find("Camera Offset/LeftHand Controller");
        //rightHandOrigin = origin.transform.Find("Camera Offset/RightHand Controller");

        //foreach (var item in GetComponentsInChildren<Renderer>())
        //    item.enabled = false;
    }

    private void Update()
    {
        if (!photonView.IsMine)
           return;

        MapPosition(head, headOrigin);
        MapPosition(leftHand, leftHandOrigin);
        MapPosition(rightHand, rightHandOrigin);

        //UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
        //UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
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
