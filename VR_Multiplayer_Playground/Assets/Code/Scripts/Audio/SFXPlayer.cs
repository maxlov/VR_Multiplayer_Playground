using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private SoundEffectSO soundEffect;

    public void Play()
    {
        soundEffect.Play(transform.position);
    }
}
