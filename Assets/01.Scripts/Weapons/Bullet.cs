using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : PoolableMono
{
    public float bulletSpeed = 5f;
    public float lifeTime = 3f;
    [SerializeField]
    private Rigidbody2D _rigidbody2d;

    private async void OnEnable()
    {
        await Task.Delay(1);

        _rigidbody2d.velocity = transform.right * bulletSpeed;

        Init();
    }

    public override void Init()
    {
        PoolManager.Instance.Push(this, lifeTime);
    }
}
