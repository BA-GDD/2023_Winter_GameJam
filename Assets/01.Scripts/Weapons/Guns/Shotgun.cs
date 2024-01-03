using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Gun
{
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private float _lifeTime;
    [Range(0f, 180f), SerializeField]
    private float _shotAngleRange = 100f;
    [SerializeField]
    private float _speedRange;
    [SerializeField]
    private int _shotsAmount = 7;

    public override void ShootProcess()
    {
        for (int i = 0; i < _shotsAmount; ++i)
        {
            Bullet bullet = PoolManager.Instance.Pop(PoolingType.PlayerBullet) as Bullet;
            bullet.bulletSpeed = _bulletSpeed;
            bullet.bulletSpeed += Random.Range(-_speedRange, _speedRange);
            bullet.lifeTime = _lifeTime;
            bullet.transform.position = firePosition.position;
            Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - bullet.transform.position;

            direction.Normalize();

            float angle = Random.Range(-_shotAngleRange * 0.5f, _shotAngleRange * 0.5f);
            bullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angle, Vector3.forward);
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
