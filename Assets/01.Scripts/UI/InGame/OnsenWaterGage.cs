using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class OnsenWaterGage : MonoBehaviour
{
    [SerializeField] private Image _gageImg;
    [SerializeField] private Image _waterImg;

    [SerializeField] private TextMeshProUGUI _percentText;

    private float _currentWaterValue;
    private float _targetWaterValue;

    private Sequence _notAllowSeq;

    private void Awake()
    {
        _gageImg.material.SetFloat("_amount", 0);
        _waterImg.material.SetFloat("_amount", 0);
    }

    public void ChangeWaterValue(float changingValue)
    {
        if(_currentWaterValue + changingValue < 0)
        {
            NotAllow();
        }
        _targetWaterValue = Mathf.Clamp(_targetWaterValue + changingValue, 0, 1f);
    }

    private void Update()
    {
        if(_currentWaterValue != _targetWaterValue)
        {
            _currentWaterValue = Mathf.Lerp(_currentWaterValue, _targetWaterValue, Time.deltaTime * 4);
            _gageImg.material.SetFloat("_amount", _currentWaterValue);
            _waterImg.material.SetFloat("_amount", _currentWaterValue);

            _percentText.text = $"{Mathf.RoundToInt(Mathf.Clamp(_currentWaterValue * 100, 0, 100))}%";
        }
    }

    public void NotAllow()
    {
        _notAllowSeq.Kill();

        RectTransform rt = (RectTransform)_gageImg.transform;
        _notAllowSeq = DOTween.Sequence();
        _notAllowSeq.Append(rt.DOShakeAnchorPos(0.2f, 40, 20));
    }
}
