using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButtonAction : MonoBehaviour
{
    [SerializeField] private OptionPanel _optionPanelPrefab;
    [SerializeField] private GameExitPanel _askGameExitPanelPrefab;

    public void EnterInGame()
    {
        UIManager.Instanace.ChangeScene(UIDefine.UIType.InGame);
    }

    public void EnterStore()
    {
        UIManager.Instanace.ChangeScene(UIDefine.UIType.Store);
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
