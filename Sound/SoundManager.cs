using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private ObjectPool objectPool;
    
    private AudioSource bgmAudioSource;
    public AudioClip bgmClip;

    [SerializeField] [Range(0f, 1f)] private float bgmVolume;
    [SerializeField] [Range(0f, 1f)] private float sfxVolume;

    private Dictionary<int, SoundSource> soundSource = new();
    
    private void Awake()
    {
        instance = this;
        objectPool = GetComponent<ObjectPool>();
        bgmAudioSource = GetComponent<AudioSource>();
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = bgmVolume;
    }

    private void Start()
    {
        ChangeBGMAudio(bgmClip);
    }

    public void ChangeOriginalBGMAudio()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.Play();
    }

    public void ChangeBGMAudio(AudioClip clip)
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        GameObject obj = instance.objectPool.SpawnFromPool("SoundSource");
        obj.SetActive(true);
        obj.GetComponent<SoundSource>().Play(clip, sfxVolume, false);
    }
    
    public void PlayLoopClip(AudioClip clip, int key)
    {
        GameObject obj = instance.objectPool.SpawnFromPool("SoundSource");
        obj.SetActive(true);
        SoundSource source = obj.GetComponent<SoundSource>();
        source.Play(clip, sfxVolume, true);
        soundSource.Add(key, source);
    }

    public void StopLoopClip(int key)
    {
        soundSource[key].Disable();
        soundSource.Remove(key);
    }
}
