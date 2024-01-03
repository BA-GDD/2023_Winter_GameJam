using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShakeFeedback : Feedback
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _amplitude = 1.0f;
    [SerializeField] private float _frequency = 1.0f;
    private CinemachineBasicMultiChannelPerlin _perlin;

    private void Awake()
    {
        _perlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void CreateFeedback()
    {
        StartCoroutine(Shake());
    }

    public override void CompleteFeedback()
    {
        _perlin.m_FrequencyGain = 0.0f;
        _perlin.m_AmplitudeGain = 0.0f;
    }

    IEnumerator Shake()
    {
        _perlin.m_AmplitudeGain = _amplitude;
        _perlin.m_FrequencyGain = _frequency;
        yield return new WaitForSeconds(_duration);
        CompleteFeedback();
    }
}
