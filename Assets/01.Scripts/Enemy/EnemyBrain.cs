using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono, IDamageable
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

    public Rigidbody2D rigidbody2d;

    public Vector2 dir;

    public UnityEvent onDieTrigger;
    UnityEvent IDamageable.OnDieTrigger => onDieTrigger;

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
        //MapManager.instance.SetTile(transform.position, TileType.Ground);

        dir = GameManager.Instance.player.position - transform.position;
        dir.Normalize();
        if(dir.x * transform.localScale.x < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale *= new Vector2(-1, 1);
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
