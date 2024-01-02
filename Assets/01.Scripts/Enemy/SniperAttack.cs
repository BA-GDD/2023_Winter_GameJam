using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : EnemyAttack
{
    private Vector3 _target;
    private bool _isChasing;

    protected override void Update()
    {
        base.Update();
        if (_isChasing)
        {
            _target = Vector2.Lerp(_target, GameManager.Instance.player.position, Time.deltaTime);
        }
    }

    public override void Attack()
    {
        _isChasing = false;
        StartCoroutine(Sniping());
    }

    private IEnumerator Sniping()
    {
        yield return new WaitForSeconds(1.0f);
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, (_target - _brain.transform.position).normalized, 100, _playerLayerMask);

        if (hit.Length > 0)
        {
            foreach (var h in hit)
            {
                if (h.transform.TryGetComponent<Player>(out Player p))
                {
                    //p.TakeDamage();
                }
            }

        }
        _attackTimer = 0;
        yield return new WaitForSeconds(0.5f);
        _isChasing = true;
    }
}
