using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : PoolableMono
{
    [HideInInspector]
    public Collider2D targetOfMissile;
    [HideInInspector]
    public ParticleSystem particle;
    [HideInInspector]
    public bool isMissileMode;
    [SerializeField]
    private Rigidbody2D _rigidbody2d;
    public float bulletSpeed = 5f;
    public float lifeTime = 3f;

    protected virtual void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    protected virtual async void OnEnable()
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
