using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSpa : Spa
{
    private float _beforeSpeed;
    public SpeedUpSpa() : base()
    {

    }

    public override void Enter()
    {
        _beforeSpeed = _player.movementSpeed;
        _player.movementSpeed = 10;
    }

    public override void Exit()
    {
        _player.movementSpeed = _beforeSpeed;
    }
}
