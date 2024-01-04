using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hovertest : MonoBehaviour
{
    public AudioClip hoverClip;

    public void PlayHoverSE()
    {
        SoundManager.Instance.Play(hoverClip, 1, 1, 1, false);
    }
}
