using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnsenWaterGage : MonoBehaviour
{
    [SerializeField] private RectTransform _waterTrm;
    private float _currentWaterValue;
    private float _distance = 250;

    public void ChangeWaterValue(float changingValue, float easingTime)
    {
        _currentWaterValue = Mathf.Clamp01(_currentWaterValue + changingValue);
        _waterTrm.DOLocalMoveY(_waterTrm.localPosition.y + _distance * changingValue, easingTime);
    }

}
