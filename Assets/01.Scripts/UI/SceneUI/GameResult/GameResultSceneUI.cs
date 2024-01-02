using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultSceneUI : SceneUIBase
{
    public bool isEndTimeLine;

    public void GoToLobby()
    {
        UIManager.Instanace.ChangeScene(UIDefine.UIType.Lobby);
    }

    private void Update()
    {
        if (!isEndTimeLine) return;

        if(Input.anyKeyDown)
        {
            GoToLobby();
        }
    }

    public override void SetUp()
    {
    }

    public override void Init()
    {
    }
}
