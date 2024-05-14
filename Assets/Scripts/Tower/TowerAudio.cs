using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip     shootSound = null;

    [SerializeField] private AudioSource   audioSource = null;

    private void Awake()
    {
        GameEvents.OnPlayerDefeated += StopPlaying;
        GameEvents.OnPlayerWon += StopPlaying;
    }

    public bool IsPlaying() => audioSource.isPlaying;

    public void PlayShootSound()
    {
        if (!audioSource.enabled)
        {
            return;
        }
        
        audioSource.PlayOneShot(shootSound);
    }

    public void StopPlaying()
    {
        if (!audioSource.enabled)
        {
            return;
        }
        
        audioSource.Stop();
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDefeated -= StopPlaying;
        GameEvents.OnPlayerWon -= StopPlaying;
    }
}