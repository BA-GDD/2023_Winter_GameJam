using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InitPointerLobbyBtn : MonoBehaviour
{
    private RectTransform _myRectTrm;
    private Image _selectMark;
    private Image _arrowMark;

    private float _normalWidth;
    private int _addStretchValue = 100;
    private float _easingTime = 0.3f;

    private Sequence InStretchSeq;
    private Sequence OutStretchSeq;

    private void Awake()
    {
        _myRectTrm = (RectTransform)transform;
        _normalWidth = _myRectTrm.sizeDelta.x;
        _selectMark = transform.Find("SelectMark").GetComponent<Image>();
        _arrowMark = _selectMark.transform.Find("ArrowMark").GetComponent<Image>();
    }

    public void OnPointerButton()
    {
        OutStretchSeq.Kill();
        _myRectTrm.sizeDelta = new Vector2(_normalWidth, _myRectTrm.sizeDelta.y);

        InStretchSeq = DOTween.Sequence();
        InStretchSeq.Append
            (
                DOTween.To(() => _myRectTrm.sizeDelta, d => _myRectTrm.sizeDelta = d,
                               new Vector2(_myRectTrm.sizeDelta.x + _addStretchValue, _myRectTrm.sizeDelta.y),
                               _easingTime)
            );
        InStretchSeq.Join(_selectMark.DOFade(1, _easingTime));
        InStretchSeq.Join(_arrowMark.DOFade(1, _easingTime));
    }

    public void OutPointerButton()
    {
        InStretchSeq.Kill();

        OutStretchSeq = DOTween.Sequence();
        OutStretchSeq.Append
            (
                DOTween.To(() => _myRectTrm.sizeDelta, d => _myRectTrm.sizeDelta = d,
                               new Vector2(_normalWidth, _myRectTrm.sizeDelta.y),
                               _easingTime)
            );
        OutStretchSeq.Join(_selectMark.DOFade(0, _easingTime));
        OutStretchSeq.Join(_arrowMark.DOFade(0, _easingTime));
    }
}
