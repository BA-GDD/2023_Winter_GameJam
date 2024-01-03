using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingMilk : PoolableMono
{
    [SerializeField] private float _fallingSecond;
    [SerializeField] private float _targetY;

    public void Fall(UnderMilk um)
    {
        int random = Random.Range(-360, 360);

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(_targetY, _fallingSecond).SetEase(Ease.InExpo).OnComplete(() => um.AddMilk()));
        seq.Join(transform.DOLocalRotateQuaternion(transform.localRotation * Quaternion.Euler(0, 0, random), 1.4f));
    }

    public override void Init()
    {
    }
}
