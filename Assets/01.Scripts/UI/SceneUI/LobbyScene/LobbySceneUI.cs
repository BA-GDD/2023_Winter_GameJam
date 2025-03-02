using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : SceneUIBase
{
    [SerializeField] private AudioClip _bgmClip;
    [SerializeField] private WeaponCard _revolverCards;
    [SerializeField] private WeaponCard _shotGunCards;
    [SerializeField] private WeaponCard _razerCards;

    public override void SetUp()
    {
        GameData data = GameManager.Instance.GameDataInstance;

        SoundManager.Instance.Play(_bgmClip,1,1,1,true,"Lobby");

        if(data.openRevolver)
        {
            _revolverCards.UnlockThisGun();
        }

        if(data.openShotgun)
        {
            _shotGunCards.UnlockThisGun();
        }

        if(data.openLaser)
        {
            _razerCards.UnlockThisGun();
        }
    }

    public override void Init()
    {
        SoundManager.Instance.Stop("Lobby");
    }
}
