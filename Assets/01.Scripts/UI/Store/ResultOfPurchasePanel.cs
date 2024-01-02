using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultOfPurchasePanel : PanelBase
{
    [SerializeField] private GameObject _textPanel;
    [SerializeField] private TextMeshProUGUI _infoText;

    public void SetText(string info)
    {
        _infoText.SetText(info);
    }

    public override void SetUpPanel()
    {
        _blackPanelEndCallback += () => Destroy(gameObject);
        ActiveBlackPanel(true);
    }

    public override void InitPanel()
    {
        _textPanel.SetActive(false);
        ActiveBlackPanel(false);
    }
}
