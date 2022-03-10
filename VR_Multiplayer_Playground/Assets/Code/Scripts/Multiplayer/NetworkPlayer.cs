using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private PhotonView photonView;

    private Transform headOrigin;
    private Transform leftHandOrigin;
    private Transform rightHandOrigin;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        XROrigin origin = FindObjectOfType<XROrigin>();

        if (!photonView.IsMine)
            return;

        headOrigin = origin.transform.Find("Camera Offset/Main Camera");
        leftHandOrigin = origin.transform.Find("Camera Offset/LeftHand Controller");
        rightHandOrigin = origin.transform.Find("Camera Offset/RightHand Controller");

        head.GetChild(0).gameObject.SetActive(false);
        rightHand.GetChild(0).gameObject.SetActive(false);
        leftHand.GetChild(0).gameObject.SetActive(false);
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
}
