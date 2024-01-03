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

    public SpriteRenderer _spriteRenderer;

    protected virtual void Awake()
    {
        attack = transform.GetComponent<EnemyAttack>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
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
        if (MapManager.Instance.CheckWater(transform.position))
        {
            MapManager.Instance.SetTile(transform.position, TileType.Ground);
        }


        dir = GameManager.Instance.player.position - transform.position;
        dir.Normalize();
        if (dir.x * transform.localScale.x < 0)
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
        MapManager.Instance.SetTile(transform.position, TileType.Water);
        StartCoroutine(Hit());
        EffectPlayer fx = PoolManager.Instance.Pop(PoolingType.EnemyExplosion) as EffectPlayer;
        fx.transform.position = transform.position;
        fx.StartPlay(5f);
        isDead = true;
    }

    private IEnumerator Hit()
    {
        Material mat = _spriteRenderer.material;
        mat.SetFloat("_blink_amount", 0.5f);
        yield return new WaitForSeconds(0.01f);
        mat.SetFloat("_blink_amount", 0.0f);
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
