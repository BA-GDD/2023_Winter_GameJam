using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitPanel : PanelBase
{
    [SerializeField] private GameObject _visualPanel;

    public void ExitGame()
    {
        Application.Quit();
    }

    public override void SetUpPanel()
    {
        _blackPanelEndCallback += () => Destroy(gameObject);
        ActiveBlackPanel(true);
    }

    public override void InitPanel()
    {
        _visualPanel.SetActive(false);
        ActiveBlackPanel(false);
    }
}
