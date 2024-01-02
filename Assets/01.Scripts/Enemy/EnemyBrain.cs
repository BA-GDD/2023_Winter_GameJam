using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBrain : PoolableMono
{
    [HideInInspector]
    public EnemyAttack attack;
    [HideInInspector]
    public bool isDead;
    public EnemyStatusSO status;
    public bool isChase;
    public bool isAnimFinised;
    public Transform firePos;

    public Action animationEvent;
    protected virtual void Awake()
    {
        attack = transform.GetComponent<EnemyAttack>();
    }

    protected async virtual void OnEnable()
    {
        await Task.Delay(1000);
    }

    protected virtual void Start()
    {
        StartChase();
    }

    protected virtual void Update()
    {
        if (isDead)
        {
            if (isChase)
            {

            }
            else
            {

            }
        }
        else
        {

        }
    }

    public override void Init()
    {

    }

    public virtual void SetDead()
    {
        isDead = true;
    }

    public virtual void StartChase()
    {
        isChase = true;
    }

    public virtual void StopChase()
    {
        isChase = false;
    }
    public void EndAnimation()
    {
        isAnimFinised = true;
    }
}
