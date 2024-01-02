using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Revolver : Gun
{
    public override void Shoot()
    {
        if (CanShoot())
        {
            Vector2 lookAtDirection = GameManager.Instance.mainCamera.ScreenToWorldPoint(Mouse.current.position.value) - firePosition.position;
            Bullet bullet = PoolManager.Instance.Pop(PoolingType.Bullet) as Bullet;

            bullet.transform.LookAt(lookAtDirection);
            base.Skill();
        }
    }

    public override void Skill()
    {
        if (CanUseSkill())
        {


            base.Skill();
        }
    }
}
