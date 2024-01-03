using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class EquipGunSystem : MonoBehaviour
{
    [Header("참조 값")]
    [SerializeField] private Transform _capybaraTrm;
    [SerializeField] private Transform _titleGroup;
    [SerializeField] private Transform _btnGroup;
    [SerializeField] private Transform _weaponBar;
    [SerializeField] private Transform _capZone;
    [SerializeField] private Animator _lobbyCapAnimator;
    [SerializeField] private Image _gunImg;

    [Header("셋팅 값")]
    [SerializeField] private Vector2 _normalCapPos;
    [SerializeField] private Vector2 _equipCapPos;
    [SerializeField] private Vector2 _weaponBarNormalPos;
    [SerializeField] private Vector2 _weaponBarMovePos;

    private bool _inEquipping = false;

    private event Action _capybaraEquipEvent;

    private void Start()
    {
        _capybaraEquipEvent += StartEquip;
    }

    public void OnPointerCapybara()
    {
        if (_inEquipping) return;
        _lobbyCapAnimator.enabled = true;
    }
    public void OutPointerCapybara()
    {
        if (_inEquipping) return;
        _lobbyCapAnimator.enabled = false;
    }
    public void OnPointerDownCapybara()
    {
        _capybaraEquipEvent?.Invoke();
    }
    public void StartEquip()
    {
        _lobbyCapAnimator.enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.Append(_capybaraTrm.DOLocalMove(_equipCapPos, 0.4f));
        seq.Join(_capZone.DOLocalMoveX(0, 0.4f));
        seq.Join(_titleGroup.DOLocalMoveX(-640, 0.2f));
        seq.Join(_btnGroup.DOLocalMoveX(-640, 0.3f));
        seq.Join(_capybaraTrm.DOLocalRotateQuaternion(Quaternion.identity, 0.2f));
        seq.Join(_weaponBar.DOLocalMove(_weaponBarMovePos, 0.3f));
        seq.AppendCallback(() => _inEquipping = true);

        _capybaraEquipEvent -= StartEquip;
        _capybaraEquipEvent += CompleteEquip;
    }
    public void CompleteEquip()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_capybaraTrm.DOLocalMove(_normalCapPos, 0.4f));
        seq.Join(_capZone.DOLocalMoveX(540, 0.4f));
        seq.Join(_titleGroup.DOLocalMoveX(0, 0.2f));
        seq.Join(_btnGroup.DOLocalMoveX(0, 0.3f));
        seq.Join(_capybaraTrm.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, -13), 0.2f));
        seq.Join(_weaponBar.DOLocalMove(_weaponBarNormalPos, 0.3f));
        seq.AppendCallback(() => _inEquipping = false);
        seq.AppendCallback(() => _lobbyCapAnimator.enabled = true);

        _capybaraEquipEvent += StartEquip;
        _capybaraEquipEvent -= CompleteEquip;
    }

    public void EquipGun(GunType type, Sprite gs, Vector2 gPos, Vector2 gScale)
    {
        GameManager.Instance.selectGunType = type;
        _gunImg.sprite = gs;
        _gunImg.transform.localPosition = gPos;
        _gunImg.transform.localScale = gScale;
    }
}
