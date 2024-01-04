using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class EnemyBrain : PoolableMono, IDamageable
{
    [HideInInspector]
    public EnemyAttack attack;
    public EnemyStatusSO status;
    public bool isChase;
    public bool isAnimFinised;
    public Transform firePos;
    private bool _isDead;
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    public Action animationEvent;

    public Rigidbody2D rigidbody2d;

    public Vector2 dir;

    public UnityEvent onDieTrigger;
    UnityEvent IDamageable.OnDieTrigger => onDieTrigger;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public AudioClip hitClip;
    public AudioClip shootClip;

    private Vector3Int _lastFootPos;
    private int _footTileCount;

    protected virtual void Awake()
    {
        attack = transform.GetComponent<EnemyAttack>();
        spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
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
        if (!_isDead && MapManager.Instance.CheckWater(transform.position, out Vector3Int pos))
        {
            if (_lastFootPos != pos)
            {
                _lastFootPos = pos;
                _footTileCount++;
                MapManager.Instance.SetTile(transform.position, TileType.Ground);
                if (_footTileCount >= 2)
                {
                    _isDead = true;
                    EnemySpawner.Instance.DeSpawnEnemy(this);
                    return;
                }
            }
        }

        dir = GameManager.Instance.player.position - transform.position;
        dir.Normalize();
        if (dir.x * transform.localScale.x < 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.localScale *= new Vector2(-1, 1);
    }

    public override void Init()
    {

    }

    public virtual void SetDead(bool isBomberMan = false)
    {
        SoundManager.Instance.Play(hitClip, 1, 1, 1, false);
        if (!isBomberMan)
        {
            MapManager.Instance.SetTile(transform.position, TileType.Water);
        }
        StartCoroutine(Hit());
        EffectPlayer fx = PoolManager.Instance.Pop(PoolingType.EnemyExplosion) as EffectPlayer;
        fx.transform.position = transform.position;
        fx.StartPlay(5f);
        _isDead = true;
    }

    private IEnumerator Hit()
    {
        Material mat = spriteRenderer.material;
        mat.SetFloat("_blink_amount", 0.5f);
        yield return new WaitForSeconds(0.03f);
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
