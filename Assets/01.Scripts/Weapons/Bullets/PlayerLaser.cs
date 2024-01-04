using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [HideInInspector]
    public ParticleSystem rootParticle;
    [HideInInspector]
    public ParticleSystem shootParticle;

    [Header("Shoot Laser Properties")]
    [SerializeField]
    private float _shootStartLifeTime = 0.5f;
    [SerializeField]
    private float _shootStartSizeX = 1f;
    [SerializeField]
    private float _shootStartSizeY = 1f;

    [Header("Skill Laser Properties")]
    [SerializeField]
    private float _skillStartLifeTime = 3f;
    [SerializeField]
    private float _skillStartSizeX = 2.5f;
    [SerializeField]
    private float _skillStartSizeY = 10f;

    private ParticleSystem.MainModule _shootMainModule;

    private void Awake()
    {
        rootParticle = GetComponent<ParticleSystem>();
        shootParticle = transform.Find("shoot").GetComponent<ParticleSystem>();
        _shootMainModule = shootParticle.main;
    }

    public void SetToShootLaser()
    {
        _shootMainModule.startLifetime = _shootStartLifeTime;
        _shootMainModule.startSizeX = _shootStartSizeX;
        _shootMainModule.startSizeY = _shootStartSizeY;
    }

    public void SetToSkillLaser()
    {
        _shootMainModule.startLifetime = _skillStartLifeTime;
        _shootMainModule.startSizeX = _skillStartSizeX;
        _shootMainModule.startSizeY = _skillStartSizeY;
    }
}
