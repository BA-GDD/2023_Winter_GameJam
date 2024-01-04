using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public enum GunType
{
    Revolver,
    Razer,
    Shotgun
}

public abstract class Gun : MonoBehaviour
{
    private readonly int _shootTriggerHash = Animator.StringToHash("shoot");
    [HideInInspector]
    public Player owner;
    [SerializeField]
    protected LayerMask enemyLayerMask;
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected GunSO gunScriptableObject;
    protected Coroutine skillProcessCoroutine;
    protected bool isSkillProcess;
    private Animator _animator;
    private Transform _gunSocket;
    private float _shootDelayTimer;
    private float _usableCapacity;
    protected float _currentSkillGauge;

    public AudioClip shootClip;

    protected Camera _mainCam;

    public event Action<float> usableCapacityChanged;
    public event Action<float> currentSkillChanged;

    private void Awake()
    {
        _mainCam = Camera.main;
        _usableCapacity = 0;
        _animator = GetComponent<Animator>();
        _gunSocket = transform.parent;
        _shootDelayTimer = gunScriptableObject.shootDelay;
        _currentSkillGauge = 0f;
    }

    protected virtual void Update()
    {
        _shootDelayTimer -= Time.deltaTime;
        Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - _gunSocket.position;

        direction.Normalize();

        if (owner.transform.localScale.x * _gunSocket.localScale.x * direction.x < 0f)
        {
            Flip();
        }

        _gunSocket.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (direction.x < 0f ? 180f : 0f), Vector3.forward);
    }

    private void OnDisable()
    {
        InitializeSkill();
    }

    public abstract void ShootProcess();

    public abstract void Skill(bool occurSkill);

    protected abstract IEnumerator SkillProcess();

    public virtual void Flip()
    {
        _gunSocket.localScale *= new Vector2(-1f, 1f);
    }

    public virtual void Reload()
    {
        _usableCapacity += gunScriptableObject.fillCapacityPerSecond * Time.deltaTime;
        _usableCapacity = Mathf.Clamp(_usableCapacity, 0f, gunScriptableObject.maximumCapacity);

        usableCapacityChanged?.Invoke(gunScriptableObject.fillCapacityPerSecond * Time.deltaTime / gunScriptableObject.maximumCapacity);
    }

    protected virtual void InitializeSkill()
    {
        if (isSkillProcess)
        {
            isSkillProcess = false;
            _currentSkillGauge = 0f;

            if (skillProcessCoroutine != null)
            {
                StopCoroutine(skillProcessCoroutine);
            }

            skillProcessCoroutine = null;

            currentSkillChanged?.Invoke(-1);
        }

    }

    public void Reload(ref bool canReload)
    {
        canReload = CanReload();

        if (canReload)
        {
            float before = _usableCapacity;
            _usableCapacity += gunScriptableObject.fillCapacityPerSecond * Time.deltaTime;
            _usableCapacity = Mathf.Clamp(_usableCapacity, 0f, gunScriptableObject.maximumCapacity);
            usableCapacityChanged?.Invoke((_usableCapacity - before) / gunScriptableObject.maximumCapacity);
        }
        else
            MapManager.Instance.ExitSpa();
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            SetShootTrigger(true);

            SoundManager.Instance.Play(shootClip, 0.7f, 1, 2, false);

            float before = _usableCapacity;
            _usableCapacity -= gunScriptableObject.useCapacityPerShoot;
            _usableCapacity = Mathf.Clamp(_usableCapacity, 0f, gunScriptableObject.maximumCapacity);
            _shootDelayTimer = gunScriptableObject.shootDelay;

            usableCapacityChanged?.Invoke(-((before - _usableCapacity) / gunScriptableObject.maximumCapacity));

            ShootProcess();
        }
    }
    protected void KillEvnetHandle()
    {
        float before = _currentSkillGauge;
        _currentSkillGauge += gunScriptableObject.fillSkillGauge;
        _currentSkillGauge = Mathf.Clamp(_currentSkillGauge, 0f, gunScriptableObject.requireSkillGauge);

        currentSkillChanged?.Invoke((_currentSkillGauge - before) / gunScriptableObject.requireSkillGauge);
    }

    protected bool CanUseSkill()
    {
        print($"{_currentSkillGauge}/{gunScriptableObject.requireSkillGauge}/{_currentSkillGauge >= gunScriptableObject.requireSkillGauge}");
        return _currentSkillGauge >= gunScriptableObject.requireSkillGauge && !isSkillProcess;
    }

    private void SetShootTrigger(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_shootTriggerHash);
        }
        else
        {
            _animator.ResetTrigger(_shootTriggerHash);
        }
    }

    private bool CanReload()
    {
        return MapManager.Instance.CheckWater(owner.transform.position, out Vector3Int pos);
    }

    private bool CanShoot()
    {
        return _usableCapacity >= gunScriptableObject.useCapacityPerShoot && _shootDelayTimer <= 0f;
    }
}
