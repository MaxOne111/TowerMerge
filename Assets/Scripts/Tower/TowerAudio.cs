using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _Shoot_Sound;

    private AudioSource _Audio_Source;

    private void Awake()
    {
        _Audio_Source = GetComponent<AudioSource>();

        GameEvents._On_Player_Defeated += StopPlaying;
        GameEvents._On_Player_Won += StopPlaying;
    }

    public bool IsPlaying() => _Audio_Source.isPlaying;

    public void PlayShootSound()
    {
        if (!_Audio_Source.enabled)
            return;
     
        _Audio_Source.PlayOneShot(_Shoot_Sound);
    }

    public void StopPlaying()
    {
        if (!_Audio_Source.enabled)
            return;

        _Audio_Source.Stop();
    }

    private void OnDisable()
    {
        GameEvents._On_Player_Defeated -= StopPlaying;
        GameEvents._On_Player_Won -= StopPlaying;
    }
}