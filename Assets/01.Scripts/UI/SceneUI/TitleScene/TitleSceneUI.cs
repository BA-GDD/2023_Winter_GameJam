using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public class TitleSceneUI : SceneUIBase
{
    [SerializeField] private AudioClip _bgmClip;
    public override void SetUp()
    {
        SoundManager.Instance.Play(_bgmClip, 1, 1, 1, true, "Title");
    }
    public override void Init()
    {
        SoundManager.Instance.Stop("Title");
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            UIManager.Instanace.ChangeSceneFade(UIType.Lobby, true);
        }
    }
}
