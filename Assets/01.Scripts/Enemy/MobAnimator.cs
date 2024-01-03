using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private readonly int _isMoveHash = Animator.StringToHash("is_move");
    private readonly int _isAttackHash = Animator.StringToHash("is_attack");
    private readonly int _attackTriggerHash = Animator.StringToHash("attack");
    private readonly int _dieTriggerHash = Animator.StringToHash("die");
    private readonly int _shootTriggerHash = Animator.StringToHash("shoot");

    public void SetShootTrigger(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_shootTriggerHash);
        }
        else
        {
            _animator.ResetTrigger(_shootTriggerHash);
        }
    }

    public void SetIsMove(bool value)
    {
        _animator.SetBool(_isMoveHash, value);
    }

    public void SetIsAttack(bool value)
    {
        _animator.SetBool(_isAttackHash, value);
    }

    public void SetAttackTrigger(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_attackTriggerHash);
        }
        else
        {
            _animator.ResetTrigger(_attackTriggerHash);
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
}
