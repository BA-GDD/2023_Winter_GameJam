using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public class TitleSceneUI : SceneUIBase
{
    public override void SetUp()
    {

    }
    public override void Init()
    {

    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            UIManager.Instanace.ChangeScene(UIType.Lobby);
        }
    }
}
