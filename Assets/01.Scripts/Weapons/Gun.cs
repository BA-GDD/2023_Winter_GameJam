using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Revolver,
    Razer,
    Shotgun
}

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected GunSO gunScriptableObject;
    private float _shootDelayTimer;
    private float _usableCapacity;
    private float _currentSkillGauge;

    protected virtual void OnEnable()
    {
        _shootDelayTimer = gunScriptableObject.shootDelay;
        _usableCapacity = gunScriptableObject.maximumCapacity;
        _currentSkillGauge = 0f;
    }

    protected virtual void Update()
    {
        _shootDelayTimer -= Time.deltaTime;
    }

    public virtual void Reload()
    {
        _usableCapacity += gunScriptableObject.fillCapacityPerSecond * Time.deltaTime;
        _usableCapacity = Mathf.Clamp(_usableCapacity, 0f, gunScriptableObject.maximumCapacity);
        _currentSkillGauge += gunScriptableObject.fillSkillGaugePerSecond * Time.deltaTime;
        _currentSkillGauge = Mathf.Clamp(_currentSkillGauge, 0f, gunScriptableObject.requireSkillGauge);
    }

    public virtual void Shoot()
    {
        _usableCapacity -= gunScriptableObject.useCapacityPerShoot;
    }

    public virtual void Skill()
    {
        _currentSkillGauge = 0f;
    }

    protected bool CanShoot()
    {
        return _usableCapacity >= gunScriptableObject.useCapacityPerShoot && _shootDelayTimer <= 0f;
    }

    protected bool CanUseSkill()
    {
        return _currentSkillGauge >= gunScriptableObject.requireSkillGauge;
    }
}
