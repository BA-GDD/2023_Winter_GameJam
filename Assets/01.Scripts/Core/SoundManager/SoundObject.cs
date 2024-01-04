using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundObject : MonoBehaviour
{
    private AudioSource source;

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioMixerGroup group,AudioClip clip, float volume = 1f, float pitch = 1f, bool loop = false)
    {
        source.outputAudioMixerGroup = group;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}
