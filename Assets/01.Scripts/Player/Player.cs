using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    private readonly int _materialHalfAmountHash = Shader.PropertyToID("_player_half_amount");
    [HideInInspector]
    public InGameSceneUI inGameSceneUI;
    [SerializeField]
    private UnityEvent _onDieTrigger;
    [SerializeField]
    private InputReader _inputReader;
    public float movementSpeed;
    [SerializeField]
    private float _dashDelay;
    [SerializeField]
    private float _dashDuration;
    [SerializeField]
    private ParticleSystem _playerDashFX;
    [SerializeField]
    private ParticleSystem _playerWalkFX;
    private Vector2 _dashDirection;
    private Material _material;
    private Rigidbody2D _rigidbody2D;
    private Transform _gunSocket;
    private Gun _equipedGun;
    private PlayerAnimator _playerAnimator;
    private bool _canReload;
    private bool _isDash;
    private bool _isDead;
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }
    private bool _isMove;
    public bool IsMove => _isMove;
    private float _dashTimer;
    UnityEvent IDamageable.OnDieTrigger => _onDieTrigger;


    private Camera _mainCam;

    public AudioClip dashClip;

    private bool _isMutheki;

    [SerializeField]
    private ParticleSystem _mutekiFX;
    [SerializeField]
    private ParticleSystem _mutekiFX_01;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _material = _playerAnimator.animator.GetComponent<SpriteRenderer>().material;
        _gunSocket = _playerAnimator.animator.transform.Find("GunSocket");
        _dashTimer = 0f;

        _mainCam = Camera.main;
    }

    IEnumerator Muteki()
    {
        yield return new WaitForSeconds(1.6f);
        _mutekiFX_01.Play();
    }

    private void Start()
    {
        EquipGun(GameManager.Instance.selectGunType);

        _inputReader.onDashEvent += Dash;
    }

    private void Update()
    {
        if(!_isMutheki && Input.GetKeyDown(KeyCode.P))
        {
            _isMutheki = true;
            _mutekiFX.Play();
            StartCoroutine(Muteki());
        }

        if (_isDead || _equipedGun == null)
        {
            return;
        }

        _dashTimer -= Time.deltaTime;

        if (transform.localScale.x * _inputReader.movementDirection.x < 0f)
        {
            Flip();
        }

        if (_inputReader.isReload)
        {
            _equipedGun?.Reload(ref _canReload);
        }
        else
        {
            _canReload = false;
        }

        if (_canReload)
        {
            Movement(_inputReader.movementDirection, movementSpeed * 0.25f);

            if (_isMove)
            {
                _material.SetFloat(_materialHalfAmountHash, -(1f / 6f * 4f + 1f / 6f * 10f / 34f));
            }
            else
            {
                _material.SetFloat(_materialHalfAmountHash, -(1f / 6f * 3f + 1f / 6f * 10f / 34f));
            }
        }
        else
        {
            if (_isDash)
            {
                if (transform.localScale.x * _dashDirection.x < 0f)
                {
                    Flip();
                }

                Movement(_dashDirection, movementSpeed * 5f);
            }
            else
            {
                Movement(_inputReader.movementDirection, movementSpeed);
            }

            _material.SetFloat(_materialHalfAmountHash, 1f);

            if (_inputReader.isSkillOccur)
            {
                _inputReader.isSkillOccur = false;
                _inputReader.isSkillPrepare = false;

                _equipedGun?.Skill(true);
            }
            else if (_inputReader.isSkillPrepare)
            {
                _equipedGun?.Skill(false);
            }
        }

        if (_playerAnimator.GetBoolValueByIndex(1) != _canReload)
        {
            if (_canReload)
            {
                MapManager.Instance.EnterSpa();
                _inputReader.onShootEvent -= _equipedGun.Shoot;
            }
            else
            {
                MapManager.Instance.ExitSpa();
                _inputReader.onShootEvent += _equipedGun.Shoot;
            }

            _equipedGun.gameObject.SetActive(!_canReload);

            _playerAnimator.animator.transform.localPosition = new Vector2(0f, -0.1f * (_canReload ? 1f : 0f));

            _playerAnimator.SetReload(_canReload);
        }
    }

    public void EquipGun(GunType gunType)
    {
        _equipedGun = _gunSocket.Find(gunType.ToString()).GetComponent<Gun>();
        _equipedGun.owner = this;
        _inputReader.onShootEvent += _equipedGun.Shoot;

        _equipedGun.gameObject.SetActive(true);
    }
    public void SetWaterGaugeHandle(OnsenWaterGage onsen)
    {
        _equipedGun.usableCapacityChanged += onsen.ChangeWaterValue;
    }
    public void DeleteWaterGaugeHandle(OnsenWaterGage onsen)
    {
        _equipedGun.usableCapacityChanged -= onsen.ChangeWaterValue;
    }

    public void SetSkillGroup(SkillBarGroup skillBarGroup)
    {
        _equipedGun.currentSkillChanged += skillBarGroup.ChangeValue;
    }
    public void DeleteSkillGroup(SkillBarGroup skillBarGroup)
    {
        _equipedGun.currentSkillChanged -= skillBarGroup.ChangeValue;
    }


    public void UnequipGun()
    {
        _inputReader.onShootEvent -= _equipedGun.Shoot;
        _equipedGun.owner = null;
        _equipedGun = null;
    }

    public void OnHitHandle()
    {
        if (_isMutheki) return;

        if (_isDead)
        {
            return;
        }

        _equipedGun.gameObject.SetActive(false);
        _isDead = true;
        _rigidbody2D.velocity = Vector3.zero;
        DeleteSkillGroup(inGameSceneUI.skillBarGroup);
        DeleteWaterGaugeHandle(inGameSceneUI.onsenWater);
        UnequipGun();

        _material.SetFloat(_materialHalfAmountHash, 1f);
        (this as IDamageable).OnHit();
        GameManager.Instance.GameEnd();
    }

    private void Dash()
    {
        if (_dashTimer <= 0f)
        {
            _dashDirection = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - transform.position;
            SoundManager.Instance.Play(dashClip, 1, 1, 2, false);

            _dashDirection.Normalize();
            var module = _playerDashFX.GetComponent<ParticleSystemRenderer>();
            if (transform.localScale.x < 0.0f)
            {

                module.flip = new Vector3(1, 0, 0);
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
        _isMove = _rigidbody2D.velocity.x != 0f || _rigidbody2D.velocity.y != 0f;


        _playerAnimator.SetMove(_isMove);
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
