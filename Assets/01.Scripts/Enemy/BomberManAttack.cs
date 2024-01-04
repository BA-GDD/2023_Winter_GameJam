using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberManAttack : EnemyAttack
{
    public override void Attack()
    {
        float posX = _brain.transform.position.x;
        float posY = _brain.transform.position.y;

        for (int x = (int)posX - 1; x <= (int)posX + 1; ++x)
        {
            for (int y = (int)posY - 1; y <= (int)posY + 1; ++y)
            {
                MapManager.Instance.SetTile(new Vector2(x, y), TileType.Ground);
            }
        }
    }
}
