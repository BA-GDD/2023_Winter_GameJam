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
        transform.DOLocalMoveY(_targetY, _fallingSecond).SetEase(Ease.InExpo).OnComplete(()=>um.AddMilk());
    }

    public override void Init()
    {
    }
}
