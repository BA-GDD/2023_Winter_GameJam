using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : EnemyAttack
{
    private ParticleSystem _shieldParticle;

    protected override void Awake()
    {
        base.Awake();
        _shieldParticle = transform.Find("P_Toon_Circle_Dissolve").GetComponent<ParticleSystem>();
        _shieldParticle.transform.position = _brain.firePos.position;
        _shieldParticle.transform.rotation = new Quaternion(180, 0, 0, 0);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        Collider2D collider = Physics2D.OverlapCircle(_brain.transform.position, 1.5f, 1 << 6);

        if (collider != null)
        {
            if (collider.gameObject.transform.TryGetComponent<Player>(out Player p))
            {
                p.OnHitHandle();
            }
        }
        _shieldParticle.Play();

        _attackTimer = 0;
    }
}
