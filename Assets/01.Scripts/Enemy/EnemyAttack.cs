using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyBrain _brain;
    protected float _attackTimer = 0f;
    protected bool _isAttack;
    public bool IsAttack
    {
        get
        {
            return _isAttack;
        }
        set
        {
            _isAttack = value;
        }
    }
    [SerializeField]
    protected LayerMask _playerLayerMask;

    protected virtual void Awake()
    {
        _brain = transform.parent.GetComponent<EnemyBrain>();
    }

    protected virtual void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    public abstract void Attack();
}
