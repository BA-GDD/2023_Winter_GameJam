using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButtonAction : MonoBehaviour
{
    [SerializeField] private OptionPanel _optionPanelPrefab;
    [SerializeField] private GameExitPanel _askGameExitPanelPrefab;

    public void EnterInGame()
    {
        Action callback = null;
        callback += () => UIManager.Instanace.ChangeSceneFade(UIDefine.UIType.InGame, true);
        callback += () => GameManager.Instance.GameStart();
        GameManager.Instance.SceneChange("Game", callback);
    }

    public void EnterStore()
    {
        UIManager.Instanace.ChangeSceneFade(UIDefine.UIType.Store, true);
    }

    public void ActiveOption()
    {
        UIManager.Instanace.CreatePanel(_optionPanelPrefab);
    }

    public void AskExitGame()
    {
        UIManager.Instanace.CreatePanel(_askGameExitPanelPrefab);
    }
}
