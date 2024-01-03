using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    //[SerializeField]
    private UnityEvent _onDieTrigger;
    [SerializeField]
    private InputReader _inputReader;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _dashDelay;
    [SerializeField]
    private float _dashDuration;
    [SerializeField]
    private ParticleSystem _playerDashFX;
    private Vector2 _dashDirection;
    private Rigidbody2D _rigidbody2D;
    private Transform _gunSocket;
    private Gun _equipedGun;
    private PlayerAnimator _playerAnimator;
    private bool _isDash;
    private bool _isDead;
    private float _dashTimer;
    UnityEvent IDamageable.OnDieTrigger => _onDieTrigger;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gunSocket = transform.Find("GunSocket");
        _playerAnimator = GetComponent<PlayerAnimator>();
        _dashTimer = 0f;
    }

    private void Start()
    {
        _inputReader.onDashEvent += Dash;

        // Debug
        EquipGun(GunType.Shotgun);
    }

    private void Update()
    {
        _dashTimer -= Time.deltaTime;

        if (transform.localScale.x * _inputReader.movementDirection.x < 0f)
        {
            Flip();
        }

        if (_isDash)
        {
            if (transform.localScale.x * _dashDirection.x < 0f)
            {
                Flip();
            }

            Movement(_dashDirection, _movementSpeed * 5f);
        }
        else
        {
            Movement(_inputReader.movementDirection, _movementSpeed);
        }
    }

    public void EquipGun(GunType gunType)
    {
        _equipedGun = _gunSocket.Find(gunType.ToString()).GetComponent<Gun>();
        _inputReader.onReloadEvent += _equipedGun.Reload;
        _inputReader.onShootEvent += _equipedGun.Shoot;
        _inputReader.onSkillEvent += _equipedGun.Skill;

        _equipedGun.gameObject.SetActive(true);
    }

    public void UnequipGun()
    {
        _equipedGun.gameObject.SetActive(false);

        _inputReader.onReloadEvent -= _equipedGun.Reload;
        _inputReader.onShootEvent -= _equipedGun.Shoot;
        _inputReader.onSkillEvent -= _equipedGun.Skill;
        _equipedGun = null;
    }

    public void OnHitHandle()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;
        _playerAnimator.SetDieTrigger(_isDead);
        UnequipGun();
        (this as IDamageable).OnHit();
    }

    private void Dash()
    {
        if (_dashTimer <= 0f)
        {
            _dashDirection = GameManager.Instance.mainCamera.ScreenToWorldPoint(Mouse.current.position.value) - transform.position;

            _dashDirection.Normalize();
            var module = _playerDashFX.GetComponent<ParticleSystemRenderer>();
            if(transform.localScale.x < 0.0f)
            {

                module.flip = new Vector3(1,0,0);
            }
            else
            {
                module.flip = new Vector3(0, 0, 0);
            }
            _playerDashFX.Play();
            StartCoroutine(DashCoroutine());
        }
    }

    private void Flip()
    {
        transform.localScale *= new Vector2(-1f, 1f);
    }

    private void Movement(Vector2 direction, float speed)
    {
        _rigidbody2D.velocity = direction * speed;

        if (_rigidbody2D.velocity.x != 0f || _rigidbody2D.velocity.y != 0f)
        {
            _playerAnimator.SetMove(true);
        }
        else
        {
            _playerAnimator.SetMove(false);
        }
    }

    private IEnumerator DashCoroutine()
    {
        _isDash = true;
        _dashTimer = _dashDelay;

        _playerAnimator.SetDashTrigger(_isDash);

        yield return new WaitForSeconds(_dashDuration);

        _isDash = false;
    }
}
