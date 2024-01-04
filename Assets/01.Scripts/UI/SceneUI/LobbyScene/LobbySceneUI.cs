using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : SceneUIBase
{
    [SerializeField] private AudioClip _bgmClip;
    public override void SetUp()
    {
        SoundManager.Instance.Play(_bgmClip,1,1,1,true,"Lobby");
    }

    public override void Init()
    {
        SoundManager.Instance.Stop("Lobby");
    }
}
