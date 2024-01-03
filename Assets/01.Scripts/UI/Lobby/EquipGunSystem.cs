using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EquipGunSystem : MonoBehaviour
{
    [Header("참조 값")]
    [SerializeField] private Transform _capybaraTrm;
    [SerializeField] private Transform _titleGroup;
    [SerializeField] private Transform _btnGroup;
    [SerializeField] private Animator _lobbyCapAnimator;

    [Header("셋팅 값")]
    [SerializeField] private Vector2 _equipCapPos;

    public void OnPointerCapybara()
    {
        _lobbyCapAnimator.enabled = true;
    }
    public void OutPointerCapybara()
    {
        _lobbyCapAnimator.enabled = false;
    }

    public void OnPointerDownCapybara()
    {
        _lobbyCapAnimator.enabled = false;
        _capybaraTrm.DOLocalMove(_equipCapPos, 0.4f);
    }
}
