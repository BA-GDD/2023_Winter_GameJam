using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GunType
{
    Revolver,
    Razer,
    Shotgun
}

public abstract class Gun : MonoBehaviour
{
    private readonly int _shootTriggerHash = Animator.StringToHash("shoot");
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected GunSO gunScriptableObject;
    private Vector2 _direction;
    private Animator _animator;
    private Transform _gunSocket;
    private float _shootDelayTimer;
    private float _usableCapacity;
    private float _currentSkillGauge;

    protected virtual void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _gunSocket = transform.parent;
        _shootDelayTimer = gunScriptableObject.shootDelay;
        _usableCapacity = gunScriptableObject.maximumCapacity;
        _currentSkillGauge = 0f;
    }

    protected virtual void Update()
    {
        _shootDelayTimer -= Time.deltaTime;
        _direction = (GameManager.Instance.mainCamera.ScreenToWorldPoint(Mouse.current.position.value) - _gunSocket.position).normalized;

        if (GameManager.Instance.player.transform.localScale.x * _gunSocket.localScale.x * _direction.x < 0f)
        {
            Flip();
        }

        _gunSocket.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + (_direction.x < 0f ? 180f : 0f), Vector3.forward);
    }

    public abstract void ShootProcess();

    public virtual void Reload()
    {
        _usableCapacity += gunScriptableObject.fillCapacityPerSecond * Time.deltaTime;
        _usableCapacity = Mathf.Clamp(_usableCapacity, 0f, gunScriptableObject.maximumCapacity);
        _currentSkillGauge += gunScriptableObject.fillSkillGaugePerSecond * Time.deltaTime;
        _currentSkillGauge = Mathf.Clamp(_currentSkillGauge, 0f, gunScriptableObject.requireSkillGauge);
    }

    public virtual void Skill()
    {
        _currentSkillGauge = 0f;
    }

    public void Flip()
    {
        _gunSocket.localScale *= new Vector2(-1f, 1f);
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            SetShootTrigger(true);

            _usableCapacity -= gunScriptableObject.useCapacityPerShoot;
            _shootDelayTimer = gunScriptableObject.shootDelay;
        }
    }

    protected bool CanShoot()
    {
        return _usableCapacity >= gunScriptableObject.useCapacityPerShoot && _shootDelayTimer <= 0f;
    }

    protected bool CanUseSkill()
    {
        return _currentSkillGauge >= gunScriptableObject.requireSkillGauge;
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
}
