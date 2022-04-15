using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using Photon.Pun;
using UnityEngine.Events;

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
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private GameObject beltParticles;
    [SerializeField] private Slider healthBar;

    public UnityEvent onTakeDamage;
    public UnityEvent onDeath;
    public UnityEvent onRespawn;

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

    public void NetworkPlayerTakeDamage(int newHealthTotal)
    {
        photonView.RPC("TakeDamage", RpcTarget.All, newHealthTotal);
    }

    [PunRPC]
    void TakeDamage(int newHealthTotal)
    {
        onTakeDamage.Invoke();
        healthBar.value = newHealthTotal;
    }

    public void NetworkPlayerDeath()
	{
        photonView.RPC("Death", RpcTarget.All);
    }

    [PunRPC]
    void Death()
	{
        onDeath.Invoke();
        if (!photonView.IsMine)
		{
            mesh.gameObject.SetActive(false);
            beltParticles.SetActive(false);
        }     
    }

    public void NetworkPlayerRespawn()
    {
        photonView.RPC("Respawn", RpcTarget.All);
    }

    [PunRPC]
    void Respawn()
    {
        onRespawn.Invoke();
        if (!photonView.IsMine)
		{
            mesh.gameObject.SetActive(true);
            beltParticles.SetActive(true);
        }
        healthBar.value = 100;
    }
}
