using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [HideInInspector]
    public Player owner;
    [SerializeField]
    protected LayerMask enemyLayerMask;
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected GunSO gunScriptableObject;
    protected bool isSkillProcess;
    private Animator _animator;
    private Transform _gunSocket;
    private float _shootDelayTimer;
    private float _usableCapacity;
    private float _currentSkillGauge;

    public AudioClip shootClip;


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
        if (isSkillProcess)
        {
            return;
        }

        _shootDelayTimer -= Time.deltaTime;
        Vector2 direction = GameManager.Instance.mainCamera.ScreenToWorldPoint(Mouse.current.position.value) - _gunSocket.position;

        direction.Normalize();

        if (owner.transform.localScale.x * _gunSocket.localScale.x * direction.x < 0f)
        {
            Flip();
        }

        _gunSocket.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (direction.x < 0f ? 180f : 0f), Vector3.forward);
    }

    public abstract void ShootProcess();

    public virtual void Skill(bool occurSkill)
    {
        _currentSkillGauge = 0f;
    }

    public void Flip()
    {
        _gunSocket.localScale *= new Vector2(-1f, 1f);
    }

    public void Reload(ref bool canReload)
    {
        canReload = CanReload();

        if (canReload)
        {
            _usableCapacity += gunScriptableObject.fillCapacityPerSecond * Time.deltaTime;
            _usableCapacity = Mathf.Clamp(_usableCapacity, 0f, gunScriptableObject.maximumCapacity);
            _currentSkillGauge += gunScriptableObject.fillSkillGaugePerSecond * Time.deltaTime;
            _currentSkillGauge = Mathf.Clamp(_currentSkillGauge, 0f, gunScriptableObject.requireSkillGauge);
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            SetShootTrigger(true);

            SoundManager.Instance.Play(shootClip, 0.7f, 1, 1, false);

            _usableCapacity -= gunScriptableObject.useCapacityPerShoot;
            _shootDelayTimer = gunScriptableObject.shootDelay;
        }
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

    private bool CanReload()
    {
        return MapManager.Instance.CheckWater(owner.transform.position);
    }

    private bool CanShoot()
    {
        return _usableCapacity >= gunScriptableObject.useCapacityPerShoot && _shootDelayTimer <= 0f;
    }
}
