using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBrain : Entity
{
    [HideInInspector]
    public EnemyAttack attack;
    [HideInInspector]
    public bool isDead;
    public EnemyStatusSO status;
    public bool isChase;

    protected virtual void Awake()
    {

    }

    protected async virtual void OnEnable()
    {
        await Task.Delay(1000);
    }

    protected virtual void Start()
    {
        StartChase();
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
}
