using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : SceneUIBase
{
    public override void SetUp()
    {

    }

    public override void Init()
    {
        SoundManager.Instance.Stop("BGM");
    }
}
