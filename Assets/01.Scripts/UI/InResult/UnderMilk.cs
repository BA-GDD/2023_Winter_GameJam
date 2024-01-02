using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderMilk : MonoBehaviour
{
    [SerializeField] private RectTransform _milkRectTransform;
    private float _targetYSize;

    public void AddMilk()
    {
        _targetYSize = _milkRectTransform.sizeDelta.y + 60;
    }

    private void Update()
    {
        _milkRectTransform.sizeDelta = new Vector2(_milkRectTransform.sizeDelta.x,
                                       Mathf.Lerp(_milkRectTransform.sizeDelta.y, 
                                       _targetYSize, Time.deltaTime * 6));
    }
}
