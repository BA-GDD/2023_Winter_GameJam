using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillBarGroup : MonoBehaviour
{
    [SerializeField] private Slider _firstSilder;
    private float _currentSkillValue;

    public void ChangeValue(float addValue)
    {
        _currentSkillValue = Mathf.Clamp(_currentSkillValue + addValue, 0f, 1f);
        print(_currentSkillValue);
    }

    private void Update()
    {
        _firstSilder.value = Mathf.Lerp(_firstSilder.value, _currentSkillValue, Time.deltaTime * 3);
    }
}
