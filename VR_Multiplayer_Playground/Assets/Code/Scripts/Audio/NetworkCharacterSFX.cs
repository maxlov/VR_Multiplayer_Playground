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
        damageEventListener = new GameEventListener();
        damageEvent.RegisterListener(damageEventListener);
        damageEventListener.Response = new UnityEvent();
        damageEventListener.Response.AddListener(PlayDamageAudio);

        deathEventListener = new GameEventListener();
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
        photonView.RPC("RPC_PlayDeathAudio", RpcTarget.All);
    }

    private void PlayDamageAudio()
    {
        photonView.RPC("RPC_PlayDamageAudio", RpcTarget.All);
    }

    public void PlayJumpAudio()
    {
        photonView.RPC("RPC_PlayJumpAudio", RpcTarget.All);
    }

    public void PlayPickupAudio()
    {
        photonView.RPC("RPC_PlayPickupAudio", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_PlayDeathAudio()
    {
        deathSFX.Play();
    }

    [PunRPC]
    private void RPC_PlayDamageAudio()
    {
        damageSFX.Play();
    }

    [PunRPC]
    private void RPC_PlayJumpAudio()
    {
        jumpSFX.Play();
    }

    [PunRPC]
    private void RPC_PlayPickupAudio()
    {
        pickupSFX.Play();
    }
}