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

    [HideInInspector] public Transform headOrigin;
    [HideInInspector] public Transform leftHandOrigin;
    [HideInInspector] public Transform rightHandOrigin;

    [Space(10)]
    public SkinnedMeshRenderer mesh;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
           return;

        mesh.enabled = false;

        MapPosition(head, headOrigin);
        MapPosition(leftHand, leftHandOrigin);
        MapPosition(rightHand, rightHandOrigin);
    }

    void MapPosition(Transform target, Transform originTransform)
    {        
        target.SetPositionAndRotation(originTransform.position, originTransform.rotation);
    }
}
