using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarningMark : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Vector3 _targetPos;
    private Transform _playerTrm;
    private void Start()
    {
        _playerTrm = GameManager.Instance.player;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.position = _playerTrm.position + (_targetPos - _playerTrm.position).normalized * 4f;
    }
    public void SetUp(Vector3 pos)
    {
        _targetPos = pos;
        SetFade();
    }

    private void SetFade()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_spriteRenderer.DOFade(0f, 0.5f).SetLoops(4, LoopType.Yoyo));
        seq.AppendCallback(() => gameObject.SetActive(false));
    }
}
