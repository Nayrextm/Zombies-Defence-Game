using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    public void PlayShoot(AudioClip clip, float volume)
    {
        if (clip == null) return;

        
        _audioSource.pitch = Random.Range(0.9f, 1.1f); 
        _audioSource.PlayOneShot(clip, volume);
    }

    public void PlayReload(AudioClip clip, float volume)
    {
        if (clip == null) return;
        
        
        _audioSource.pitch = 1.0f; 
        _audioSource.PlayOneShot(clip, volume);
    }

    public void PlayEmptyClick(AudioClip clip, float volume)
    {
        if (clip == null) return;

        _audioSource.pitch = Random.Range(0.95f, 1.05f);
        _audioSource.PlayOneShot(clip, volume);
    }
}