using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugSound : MonoBehaviour
{
    public AudioClip debugClip;
    public float volume;
    public float pitch;

    [Range(0,2)]
    public int channel;

    [ContextMenu("PlaySound")]
    public void DebugPlay()
    {
        SoundManager.Instance.Play(debugClip,volume,pitch,channel);
    }

    public void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            DebugPlay();
        }
    }
}
