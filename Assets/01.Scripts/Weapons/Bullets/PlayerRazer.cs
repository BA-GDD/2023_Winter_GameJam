using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRazer : MonoBehaviour
{
    [HideInInspector]
    public ParticleSystem particle;
    private float _lifeTime;

    private void Awake()
    {
        particle = transform.Find("line").GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        var mainParticle = particle.main;
        _lifeTime = mainParticle.startLifetime.constant;

        particle.Play();
        StartCoroutine(RazerLifeTime());
    }

    private IEnumerator RazerLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);
    }
}
