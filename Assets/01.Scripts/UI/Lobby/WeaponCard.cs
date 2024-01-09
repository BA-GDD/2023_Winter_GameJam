using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private GameObject _unlockMark;
    private bool _canEquip;
    [SerializeField] private UnityEvent<GunType, Sprite, Vector2, Vector2> _gunEquipEvent;

    public GunType myType;
    public Sprite gunSprite;
    public Vector2 gunImgPos;
    public Vector2 gunScale;

    private void Start()
    {
        if(GameManager.Instance.selectGunType == myType)
        {
            SelectThisWeapon();
        }
    }

    public void UnlockThisGun()
    {
        _canEquip = true;
        _unlockMark.SetActive(false);
    }

    public void SelectThisWeapon()
    {
        if (!_canEquip) return;
        _gunEquipEvent?.Invoke(myType, gunSprite, gunImgPos, gunScale);
        GameManager.Instance.GameDataInstance.equipedGun = myType.ToString();
        GameManager.Instance.SaveData();
    }
}
