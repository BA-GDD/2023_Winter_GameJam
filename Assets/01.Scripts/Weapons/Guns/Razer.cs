using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Razer : Gun
{
    [SerializeField]
    private LayerMask _enemyLayerMask;

    public override void ShootProcess()
    {
        Bullet razer = PoolManager.Instance.Pop(PoolingType.PlayerRazer) as Bullet;
        razer.transform.position = firePosition.transform.position;
        Vector2 direction = GameManager.Instance.mainCamera.ScreenToWorldPoint(Mouse.current.position.value) - razer.transform.position;

        direction.Normalize();

        razer.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);

        Physics2D.RaycastAll(razer.transform.position, direction, razer.particle.main.startSizeX.constant, _enemyLayerMask).ToList().ForEach(enemy =>
        {
            enemy.transform.GetComponent<MobBrain>().OnHitHandle();
        });
    }

    public override void Skill()
    {
        if (CanUseSkill())
        {


            base.Skill();
        }
    }
}
