using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MilkStamp : MonoBehaviour
{
    [SerializeField] private Transform _stampFX;
    [SerializeField] private TextMeshProUGUI _milkCountText;
    private RectTransform _myRect;
    private Vector2 _normalValue;

    public int count;

    private void Awake()
    {
        _myRect = (RectTransform)transform;
        _normalValue = _myRect.localScale;
        _myRect.localScale = _normalValue * 1.3f;
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(_normalValue, 0.4f).SetEase(Ease.InQuart));
        seq.AppendInterval(0.2f);
        seq.AppendCallback(() => SetText());
        seq.AppendCallback(() =>
        {
            Instantiate(_stampFX, transform);
        });
    }

    private void SetText()
    {
        _milkCountText.gameObject.SetActive(true);
        _milkCountText.text = $"X {count}";
    }
}
