using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSpa : Spa
{
    private float _beforeSpeed;
    public SpeedUpSpa() : base()
    {
        _beforeSpeed = _player.movementSpeed;
    }

    public override void Enter()
    {
        _player.movementSpeed = 60;
    }

    public override void Exit()
    {
        _player.movementSpeed = _beforeSpeed;
    }
}
