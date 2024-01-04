using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public abstract class PanelBase : MonoBehaviour
{
    protected event Action _blackPanelEndCallback = null;
    [SerializeField] private Image _blackPanel;
    private float _blackPanelEasingTime = 0.3f;

    public abstract void SetUpPanel();
    public abstract void InitPanel();

    public List<Button> FindButtonInPanel()
    {
        List<Button> btns = new List<Button>();
        foreach(Transform t in transform)
        {
            if(t.TryGetComponent<Button>(out Button btn))
            {
                btns.Add(btn);
            }
        }

        return btns;
    }

    public void ActiveBlackPanel(bool isActive)
    {
        if (_blackPanel == null)
        {
            Debug.LogError($"{this}'s BlackPanel is Not Exist");
            return;
        }

        _blackPanel.color = new Color(0, 0, 0, 0);

        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(_blackPanel.DOFade(Convert.ToInt32(isActive) * 0.5f, _blackPanelEasingTime));

        if(!isActive)
            seq.AppendCallback(() => _blackPanelEndCallback?.Invoke());
    }
}
