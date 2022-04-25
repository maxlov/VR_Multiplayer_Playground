using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class NetworkCharacterSFX : MonoBehaviourPun
{
    [Header("Damage SFX")]
    public GameEvent damageEvent;
    public SoundEffectSO damageSFX;
    private GameEventListener damageEventListener;

    [Header("Death SFX")]
    public GameEvent deathEvent;
    public SoundEffectSO deathSFX;
    private GameEventListener deathEventListener;

    [Header("Pickup SFX")]
    public SoundEffectSO pickupSFX;

    [Header("Jump SFX")]
    public SoundEffectSO jumpSFX;

    void Start()
    {
        damageEventListener = gameObject.AddComponent<GameEventListener>();
        damageEvent.RegisterListener(damageEventListener);
        damageEventListener.Response = new UnityEvent();
        damageEventListener.Response.AddListener(PlayDamageAudio);

        deathEventListener = gameObject.AddComponent<GameEventListener>();
        deathEvent.RegisterListener(deathEventListener);
        deathEventListener.Response = new UnityEvent();
        deathEventListener.Response.AddListener(PlayDeathAudio);
    }

    private void OnDisable()
    {
        deathEvent.UnregisterListener(deathEventListener);
        damageEvent.UnregisterListener(damageEventListener);
    }

    private void PlayDeathAudio()
    {
        photonView.RPC("RPC_PlayDeathAudio", RpcTarget.All, transform.position);
    }

    private void PlayDamageAudio()
    {
        photonView.RPC("RPC_PlayDamageAudio", RpcTarget.All, transform.position);
    }

    public void PlayJumpAudio()
    {
        photonView.RPC("RPC_PlayJumpAudio", RpcTarget.All, transform.position);
    }

    public void PlayPickupAudio()
    {
        photonView.RPC("RPC_PlayPickupAudio", RpcTarget.All, transform.position);
    }

    [PunRPC]
    private void RPC_PlayDeathAudio(Vector3 position)
    {
        deathSFX.Play(position);
    }

    [PunRPC]
    private void RPC_PlayDamageAudio(Vector3 position)
    {
        damageSFX.Play(position);
    }

    [PunRPC]
    private void RPC_PlayJumpAudio(Vector3 position)
    {
        jumpSFX.Play(position);
    }

    [PunRPC]
    private void RPC_PlayPickupAudio(Vector3 position)
    {
        pickupSFX.Play(position);
    }
}