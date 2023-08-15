using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using QuantA.Utils;

[Serializable]
public class AudioData
{
    public string name;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
    public float volume;
    public float pitch;
    public bool isLoop;
}

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioData[] audioDataArray;

    [SerializeField]
    private AudioMixerGroup mixer;
    public void Start()
    {
        this.Init();
        this.Play("BGM");
    }

    public override void Init()
    {
        base.Init();

        foreach (var audio in this.audioDataArray)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.isLoop;
        }
    }

    public void Play(string name, bool enableMixer = false)
    {
        var audio = this.GetAudioByName(name);

        if (audio != null)
        {
            this.EnableMixer(enableMixer ,audio.source);

            audio.source.Play();
        }
    }

    public void Stop(string name)
    {
        var audio = this.GetAudioByName(name);

        if (audio != null)
        {
            audio.source.Stop();
        }
    }   

    private AudioData GetAudioByName(string name)
    {
        return Array.Find(this.audioDataArray, audioData => audioData.name == name);
    }

    private void EnableMixer(bool enable, AudioSource source)
    {
        if(enable == true)
        {
            source.outputAudioMixerGroup = this.mixer;
        }

        else
        {
            source.outputAudioMixerGroup = null;
        }

    }
}
