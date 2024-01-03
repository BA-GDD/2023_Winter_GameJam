using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    public static float masterVolume = 0.0f;
    public static float bgmVolume = 0.0f;
    public static float sfxVolume = 0.0f;

    [Header("필요 에셋")]
    public AudioMixer audioMixerMaster;
    public AudioMixerGroup[] audioMixers;
    public GameObject audioObject;

    public Dictionary<string,SoundObject> soundObjects;

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

    private void Start()
    {
        audioMixerMaster.SetFloat("master", masterVolume);
        audioMixerMaster.SetFloat("bgm", bgmVolume);
        audioMixerMaster.SetFloat("sfx", sfxVolume);
    }

    public void Play(AudioClip clip, float volume = 1f, float pitch = 1f, int channel = 0, bool loop=false, string name = "")
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
        obj.Play(audioMixers[channel], clip, volume, pitch, loop);
        if (!loop)
        {
            StartCoroutine(DQ(clip.length, obj));
        }
        else
        {
            soundObjects.Add(name,obj);
        }
    }

    public void Stop(string name)
    {
        soundObjects[name].Stop();
        soundObjects[name].gameObject.SetActive(false);
        audioQueue.Enqueue(soundObjects[name]);
        soundObjects.Remove(name);
    }

    IEnumerator DQ(float time, SoundObject obj)
    {
        yield return new WaitForSeconds(time + 0.1f);
        obj.gameObject.SetActive(false);
        audioQueue.Enqueue(obj);
    }

    public void AudioMixerValueChanged(int channel)
    {
        switch (channel)
        {
            case 0://여기는 Master조절
                audioMixerMaster.SetFloat("master", masterVolume);
                break;
            case 1://여기는 BGM조절
                audioMixerMaster.SetFloat("bgm", bgmVolume);
                break;
            case 2://여기는 SFX조절
                audioMixerMaster.SetFloat("sfx", sfxVolume);
                break;
            default:
                print("잘못된 값!");
                break;
        }
    }
}
