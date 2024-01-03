using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private readonly int _isMoveHash = Animator.StringToHash("isMove");
    private readonly int _isReloadHash = Animator.StringToHash("isReload");
    private readonly int _dashTriggerHash = Animator.StringToHash("dash");
    private readonly int _dieTriggerHash = Animator.StringToHash("die");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMove(bool value)
    {
        _animator.SetBool(_isMoveHash, value);
    }

    public void SetReload(bool value)
    {
        _animator.SetBool(_isReloadHash, value);
    }

    public void SetDashTrigger(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_dashTriggerHash);
        }
        else
        {
            _animator.ResetTrigger(_dashTriggerHash);
        }
    }

    public void SetDieTrigger(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_dieTriggerHash);
        }
        else
        {
            _animator.ResetTrigger(_dieTriggerHash);
        }
    }

    public bool GetBoolValueByIndex(int index)
    {
        return _animator.GetBool(_animator.GetParameter(index).nameHash);
    }

    public float GetFloatValueByIndex(int index)
    {
        return _animator.GetFloat(_animator.GetParameter(index).nameHash);
    }
}
