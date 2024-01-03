using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    private ParticleSystem _meleeParticle;

    protected override void Awake()
    {
        base.Awake();
        _meleeParticle = transform.Find("P_Toon_Circle_Dissolve").GetComponent<ParticleSystem>();
        _meleeParticle.transform.position = _brain.transform.position;
        _meleeParticle.transform.rotation = new Quaternion(180, 0, 0, 0);
    }
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        Collider2D collider = Physics2D.OverlapCircle(_brain.transform.position, 1.5f, 1 << 6);  
        
        if(collider != null)
        {
            if (collider.gameObject.transform.TryGetComponent<Player>(out Player p))
            {
                p.OnHitHandle();
            }
        }
        _meleeParticle.Play();

        _attackTimer = 0;
    }
}
