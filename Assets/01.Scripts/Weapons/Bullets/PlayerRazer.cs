using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRazer : MonoBehaviour
{
    [HideInInspector]
    public ParticleSystem particle;

    [Header("Shoot Razer Properties")]
    [SerializeField]
    private float _shootStartLifeTime = 0.5f;
    [SerializeField]
    private float _shootStartSizeX = 10f;
    [SerializeField]
    private float _shootStartSizeY = 0.1f;

    [Header("Skill Razer Properties")]
    [SerializeField]
    private float _skillStartLifeTime = 3f;
    [SerializeField]
    private float _skillStartSizeX = 25f;
    [SerializeField]
    private float _skillStartSizeY = 1f;

    private ParticleSystem.MainModule _mainParticle;

    private void Awake()
    {
        particle = transform.Find("line").GetComponent<ParticleSystem>();
        _mainParticle = particle.main;
    }

    public void SetToShootRazer()
    {
        _mainParticle.startLifetime = _shootStartLifeTime;
        _mainParticle.startSizeX = _shootStartSizeX;
        _mainParticle.startSizeY = _shootStartSizeY;
    }

    public void SetToSkillRazer()
    {
        _mainParticle.startLifetime = _skillStartLifeTime;
        _mainParticle.startSizeX = _skillStartSizeX;
        _mainParticle.startSizeY = _skillStartSizeY;
    }
}
