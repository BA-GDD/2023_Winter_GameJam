using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerRazer : Bullet
{
    protected override void Awake()
    {
        particle = transform.Find("line").GetComponent<ParticleSystem>();
    }

    protected override async void OnEnable()
    {
        await Task.Delay(1);

        Init();
    }

    public override void Init()
    {
        var mainParticle = particle.main;
        lifeTime = mainParticle.startLifetime.constant;

        GetComponent<ParticleSystem>().Play();
        PoolManager.Instance.Push(this, lifeTime);
    }
}
