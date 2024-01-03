using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Revolver : Gun
{
    public override void ShootProcess()
    {
        PoolableMono bullet = PoolManager.Instance.Pop(PoolingType.PlayerBullet);
        bullet.transform.position = firePosition.position;
        Vector2 direction = (_mainCam.ScreenToWorldPoint(Mouse.current.position.value) - bullet.transform.position);

        direction.Normalize();

        bullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    public override void Skill()
    {
        if (CanUseSkill())
        {


            base.Skill();
        }
    }
}
