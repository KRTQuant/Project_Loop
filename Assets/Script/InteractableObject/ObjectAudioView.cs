using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectAudioView : MonoBehaviour
{
    public AudioData[] audioDataArray;

    void Start()
    {
        foreach (var audio in this.audioDataArray)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.isLoop;
        }
    }

    public void Play(string name)
    {
        var audio = this.GetAudioByName(name);

        if(audio != null) 
        {
            audio.source.Play();
        }
    }

    public void Stop(string name)
    {
        var audio = this.GetAudioByName(name);

        if(audio != null) 
        {
            audio.source.Stop();
        }
    }

    private AudioData GetAudioByName(string name)
    {
        return Array.Find(this.audioDataArray, audioData => audioData.name == name);
    }
}
