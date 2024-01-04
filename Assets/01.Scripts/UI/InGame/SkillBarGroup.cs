using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillBarGroup : MonoBehaviour
{
    [SerializeField] private Image _skillGage;
    private Material _gageMat => _skillGage.material;
    private float _currentSkillValue;

    public void ChangeValue(float addValue)
    {
        _currentSkillValue = Mathf.Clamp(_currentSkillValue + addValue, 0f, 1f);
    }

    private void Update()
    {
        _gageMat.SetFloat("_Fill", Mathf.Lerp(_gageMat.GetFloat("_Fill"), _currentSkillValue, Time.deltaTime * 3));
    }
}
