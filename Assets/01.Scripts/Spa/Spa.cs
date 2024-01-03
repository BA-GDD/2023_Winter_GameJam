using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spa
{
    protected Player _player;
    public Spa()
    {
        //_player = GameManager.Instance.player.GetComponent<Player>();
    }
    public abstract void Enter();
}
