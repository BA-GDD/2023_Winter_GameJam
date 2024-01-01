using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject 
{
    public Vector3Int position;
    public Vector3Int direction;
    public float chanceToChange;

    public WalkerObject(Vector3Int pos, Vector3Int dir, float chanceToChange)
    {
        position = pos;
        direction = dir;
        this.chanceToChange = chanceToChange;
    }
}
