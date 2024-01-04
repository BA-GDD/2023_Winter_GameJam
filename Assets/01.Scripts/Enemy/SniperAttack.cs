using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : EnemyAttack
{
    private LineRenderer _line;
    private Vector3 _target;
    private bool _isChasing;
    private bool _isAiming;
    private ParticleSystem _sniperParticle;
    private ParticleSystem.MainModule _mainModule;

    private void OnDisable()
    {
        _line.SetPosition(1, _brain.firePos.position);
        _line.enabled = false;
    }

    protected override void Awake()
    {
        base.Awake();
        _line = GetComponent<LineRenderer>();
        _sniperParticle = transform.Find("P_sniper").GetComponent<ParticleSystem>();
        _mainModule = _sniperParticle.main;
        _sniperParticle.transform.position = _brain.firePos.position;
    }

    private void OnEnable()
    {
        _line.SetPosition(1, _brain.firePos.position);
        _line.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        if (_isChasing)
        {
            _target = Vector2.Lerp(_target, GameManager.Instance.player.position, Time.deltaTime);
        }
        if (_isAiming)
        {
            Aim();
        }
    }

    public override void Attack()
    {
        _isChasing = false;
        _isAiming = true;
        _line.enabled = _isAiming;
        StartCoroutine(Sniping());
    }

    private void Aim()
    {
        _line.SetPosition(0, _brain.firePos.position);
        _line.SetPosition(1, (GameManager.Instance.player.position - _brain.firePos.position).normalized * 100);
    }

    private IEnumerator Sniping()
    {
        yield return new WaitForSeconds(0.5f);

        _isAiming = false;
        _line.enabled = _isAiming;
        (_brain as MobBrain).Animator.SetShootTrigger(true);

        yield return new WaitForSeconds(0.5f);

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, (_target - _brain.transform.position).normalized, 100, _playerLayerMask);

        _mainModule.startSizeX = 100;
        Vector2 direction = _line.GetPosition(1) - _brain.firePos.position;
        direction.Normalize();

        _sniperParticle.gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);

        if (hit.Length > 0)
        {
            foreach (var h in hit)
            {
                if (h.transform.TryGetComponent<Player>(out Player p))
                {
                    _mainModule.startSizeX = Vector2.Distance(_brain.firePos.position, _line.GetPosition(1));
                    p.OnHitHandle();
                }
            }

        }
        _line.SetPosition(1, _brain.firePos.position);
        _sniperParticle.Play();
        SoundManager.Instance.Play(_brain.shootClip, 1, 1, 1, false);
        _attackTimer = 0;
        yield return new WaitForSeconds(0.5f);
        _isChasing = true;
    }
}
