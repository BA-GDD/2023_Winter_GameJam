using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneUI : SceneUIBase
{
    private Player _player;
    public override void SetUp()
    {
        _player = FindObjectOfType<Player>();
        _player.EquipGun(GameManager.Instance.selectGunType);
    }

    public override void Init()
    {
        _player.UnequipGun();
    }
}
