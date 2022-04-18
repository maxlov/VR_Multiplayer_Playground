using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
public class SoundEffectSO : ScriptableObject
{
    [Tooltip("Selects random from among clips")]
    [SerializeField] private AudioClip[] clips;
    [Space(10)]

    [Tooltip("Min and Max volume, random value in range is used. 0 - 1")]
    [SerializeField] Vector2 volume = new Vector2(0.5f, 0.5f);
    [Tooltip("Min and Max pitch, random value in range is used. 0 - 3")]
    [SerializeField] Vector2 pitch = new Vector2(1, 1);
    [Tooltip("0.0 full 2D sound, 1.0 full 3D sound")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float spatialBlend = 0f;

    [SerializeField] AudioMixerGroup audioMixerGroup;

    [Space(10)]
    [SerializeField] private SoundClipPlayOrder playOrder;
    [SerializeField] private int playIndex = 0;

    private AudioClip GetAudioClip()
    {
        var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

        switch (playOrder)
        {
            case SoundClipPlayOrder.in_order:
                playIndex = (playIndex + 1) % clips.Length;
                break;
            case SoundClipPlayOrder.random:
                playIndex = Random.Range(0, clips.Length);
                break;
            case SoundClipPlayOrder.reverse:
                playIndex = (playIndex + clips.Length - 1) % clips.Length;
                break;
        }

        return clip;
    }

    public AudioSource Play(Vector3 position)
    {
        if (clips.Length < 1)
        {
            Debug.Log($"Missing sound clips for {name}");
            return null;
        }

        var _obj = new GameObject($"Sound_{name}", typeof(AudioSource));
        var source = _obj.GetComponent<AudioSource>();

        _obj.transform.position = position;
        source.clip = GetAudioClip();
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);
        source.spatialBlend = spatialBlend;
        source.outputAudioMixerGroup = audioMixerGroup;
        
        source.Play();
        Destroy(source.gameObject, source.clip.length / source.pitch);

        return source;
    }

    public AudioSource Play()
    {
        return Play(Vector3.zero);
    }

    enum SoundClipPlayOrder
    {
        random,
        in_order,
        reverse
    }
}
