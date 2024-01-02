using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [Header("저장값")]
    public static float masterVolume = 0.0f;
    public static float bgmVolume = 0.0f;
    public static float sfxVolume = 0.0f;

    [Header("필요 에셋")]
    public AudioMixerGroup[] audioMixer;
    public GameObject audioObject;

    Queue<SoundObject> audioQueue = new Queue<SoundObject>();
    
    private void Awake()
    {
        for (int i = 0; i < 15; i++)
        {
            SoundObject obj = Instantiate(audioObject, transform).GetComponent<SoundObject>();
            obj.gameObject.SetActive(false);
            audioQueue.Enqueue(obj);
        }
    }
    public void Play(AudioClip clip, float volume = 1f, float pitch = 1f, int channel = 0)
    {
        SoundObject obj = null;
        if (audioQueue.Count > 0)
        {
            obj = audioQueue.Dequeue();
        }
        else
        {
            obj = Instantiate(audioObject, transform).GetComponent<SoundObject>();
        }

        obj.gameObject.SetActive(true);
        obj.Play(audioMixer[channel], clip, volume, pitch);
        StartCoroutine(DQ(clip.length, obj));
    }

    IEnumerator DQ(float time, SoundObject obj)
    {
        yield return new WaitForSeconds(time + 0.1f);
        obj.gameObject.SetActive(false);
        audioQueue.Enqueue(obj);
    }
}
