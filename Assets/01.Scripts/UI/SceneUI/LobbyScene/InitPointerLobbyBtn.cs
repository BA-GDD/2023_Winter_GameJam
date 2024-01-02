using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InitPointerLobbyBtn : MonoBehaviour
{
    private RectTransform _myRectTrm;
    private float _normalWidth;
    private int _addStretchValue = 100;
    private float _easingTime = 0.3f;

    private Tween InStretch;
    private Tween OutStretch;

    private void Awake()
    {
        _myRectTrm = (RectTransform)transform;
        _normalWidth = _myRectTrm.sizeDelta.x;
    }

    public void OnPointerButton()
    {
        OutStretch.Kill();
        _myRectTrm.sizeDelta = new Vector2(_normalWidth, _myRectTrm.sizeDelta.y);

        InStretch = DOTween.To(() => _myRectTrm.sizeDelta, d => _myRectTrm.sizeDelta = d,
                               new Vector2(_myRectTrm.sizeDelta.x + _addStretchValue, _myRectTrm.sizeDelta.y),
                               _easingTime);
        InStretch.Play();
    }

    public void OutPointerButton()
    {
        InStretch.Kill();
        OutStretch = DOTween.To(() => _myRectTrm.sizeDelta, d => _myRectTrm.sizeDelta = d,
                               new Vector2(_normalWidth, _myRectTrm.sizeDelta.y),
                               _easingTime);
        OutStretch.Play();
    }
}
