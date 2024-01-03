using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Razer : Gun
{
    [SerializeField]
    private LayerMask _enemyLayerMask;
    [SerializeField]
    private PlayerRazer _razerEffect;

    public override void ShootProcess()
    {
        _razerEffect.gameObject.SetActive(true);

        _razerEffect.transform.position = firePosition.transform.position;
        Vector2 direction = GameManager.Instance.mainCamera.ScreenToWorldPoint(Mouse.current.position.value) - _razerEffect.transform.position;

        direction.Normalize();

        _razerEffect.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);

        Physics2D.RaycastAll(_razerEffect.transform.position, direction, _razerEffect.particle.main.startSizeX.constant, _enemyLayerMask).ToList().ForEach(enemy =>
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
