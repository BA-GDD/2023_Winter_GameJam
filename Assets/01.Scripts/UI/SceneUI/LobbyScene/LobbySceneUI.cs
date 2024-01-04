using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : SceneUIBase
{
    [SerializeField] private WeaponCard _revolverCards;
    [SerializeField] private WeaponCard _shotGunCards;
    [SerializeField] private WeaponCard _razerCards;

    public override void SetUp()
    {
        GameData data = GameManager.Instance.GameData;

        if(data.openRevolver)
        {
            _revolverCards.UnlockThisGun();
        }

        if(data.Shotgun)
        {
            _shotGunCards.UnlockThisGun();
        }

        if(data.Razer)
        {
            _razerCards.UnlockThisGun();
        }
    }

    public override void Init()
    {
        //SoundManager.Instance.Stop("BGM");
    }
}
