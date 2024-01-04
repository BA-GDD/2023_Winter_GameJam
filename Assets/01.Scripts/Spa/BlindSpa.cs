using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlindSpa : Spa
{
    private Light2D _light;
    public BlindSpa() : base() 
    {
        _light = GameObject.Find("GlobalLight").GetComponent<Light2D>();
    }

    public override void Enter()
    {
        _light.enabled = false;
    }

    public override void Exit()
    {
        _light.enabled = true;
    }
}
