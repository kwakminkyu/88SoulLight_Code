using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioClip clip, float sfxVolume, bool loop)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        if (loop)
            _audioSource.loop = true;
        else
            _audioSource.loop = false;
        
        CancelInvoke();
        _audioSource.clip = clip;
        _audioSource.volume = sfxVolume;
        _audioSource.Play();

        Invoke("Disable", clip.length + 2);
    }
    
    public void Disable()
    {
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}
