using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spa
{
    protected Player player;
    public Spa()
    {
        //player = GameManager.instance
    }
    public abstract void Enter();
}
