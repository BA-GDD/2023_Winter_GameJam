using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameExitPanel : PanelBase
{
    [SerializeField] private GameObject _visualPanel;
    [SerializeField] private Button _acceptBtn;
    [SerializeField] TextMeshProUGUI _infoText;

    Action callback;

    public override void SetUpPanel()
    {
        switch (UIManager.Instanace.currentUIType)
        {
            case UIDefine.UIType.Title:
                break;
            case UIDefine.UIType.Lobby:
                {
                    _acceptBtn.onClick.AddListener(() => Application.Quit());
                }
                break;
            case UIDefine.UIType.InGame:
                {
                    callback = null;
                    callback += () => UIManager.Instanace.ChangeScene(UIDefine.UIType.Lobby);
                    _acceptBtn.onClick.AddListener(() => GameManager.Instance.SceneChange("Intro", callback));
                    _acceptBtn.onClick.AddListener(InitPanel);
                }
                break;
            case UIDefine.UIType.GameResult:
                break;
            case UIDefine.UIType.Store:
                break;
            case UIDefine.UIType.Prologue:
                break;
        }

        _blackPanelEndCallback += () => Destroy(gameObject);
        ActiveBlackPanel(true);
    }

    public void SetText(string info)
    {
        _infoText.text = info;
    }

    public override void InitPanel()
    {
        Debug.Log(1);
        _visualPanel.SetActive(false);
        ActiveBlackPanel(false);
    }
}
