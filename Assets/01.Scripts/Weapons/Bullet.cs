using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : PoolableMono
{
    [HideInInspector]
    public ParticleSystem particle;
    [SerializeField]
    private Rigidbody2D _rigidbody2d;
    public float bulletSpeed = 5f;
    public float lifeTime = 3f;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private async void OnEnable()
    {
        await Task.Delay(1);

        _rigidbody2d.velocity = transform.right * bulletSpeed;

        Init();
    }

    public override void Init()
    {
        var mainParticle = particle.main;
        mainParticle.startLifetime = lifeTime;

        particle.Play();
        PoolManager.Instance.Push(this, lifeTime);
    }
}
