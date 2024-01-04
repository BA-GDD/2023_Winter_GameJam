using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private readonly int _isMoveHash = Animator.StringToHash("isMove");
    private readonly int _isReloadHash = Animator.StringToHash("isReload");
    private readonly int _dashTriggerHash = Animator.StringToHash("dash");
    private readonly int _dieTriggerHash = Animator.StringToHash("die");
    public Animator animator;

    private void Awake()
    {
        animator = transform.Find("Visual").GetComponent<Animator>();
    }

    public void SetMove(bool value)
    {
        animator.SetBool(_isMoveHash, value);
    }

    public void SetReload(bool value)
    {
        animator.SetBool(_isReloadHash, value);
    }

    public void SetDashTrigger(bool value)
    {
        if (value)
        {
            animator.SetTrigger(_dashTriggerHash);
        }
        else
        {
            animator.ResetTrigger(_dashTriggerHash);
        }
    }

    public void SetDieTrigger(bool value)
    {
        if (value)
        {
            animator.SetTrigger(_dieTriggerHash);
        }
        else
        {
            animator.ResetTrigger(_dieTriggerHash);
        }
    }

    public bool GetBoolValueByIndex(int index)
    {
        return animator.GetBool(animator.GetParameter(index).nameHash);
    }

    public float GetFloatValueByIndex(int index)
    {
        return animator.GetFloat(animator.GetParameter(index).nameHash);
    }
}
