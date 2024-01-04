using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class ChangeSceneFade : MonoBehaviour
{
    [SerializeField] private Image _myFadePanel;
    [SerializeField] private float _easingTime;

    public void FadeStart(Action sceneChangeCallback)
    {
        _myFadePanel.raycastTarget = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(_myFadePanel.DOFade(1, _easingTime));
        seq.AppendCallback(() =>
        {
            sceneChangeCallback?.Invoke();
        });
        seq.AppendInterval(0.2f);
        seq.Append(_myFadePanel.DOFade(0, _easingTime));
        seq.AppendCallback(() => _myFadePanel.raycastTarget = false);
    }
}
