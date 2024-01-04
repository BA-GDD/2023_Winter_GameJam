using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreSceneUI : SceneUIBase
{
    [SerializeField] private ResultOfPurchasePanel _resultPanel;
    [SerializeField] private TextMeshProUGUI _milkCountText;

    public override void SetUp()
    {
        _milkCountText.text = GameManager.Instance.GameData.milkCoount.ToString();
    }

    public override void Init()
    {

    }

    public void ExitToLobby()
    {
        UIManager.Instanace.ChangeSceneFade(UIDefine.UIType.Lobby, true);
    }

    public void PurchaseThisGun(GunSO target)
    {
        ResultOfPurchasePanel rpp = (ResultOfPurchasePanel)UIManager.Instanace.CreatePanel(_resultPanel, true);
        if (GameManager.Instance.GameData.milkCoount >= target.priceValue)
        {
            GameManager.Instance.GameData.milkCoount -= target.priceValue;
            rpp.SetText($"{target.gunName} 구매를\n 성공하셨습니다.");

            switch (target.myType)
            {
                case GunType.Revolver:
                    GameManager.Instance.GameData.openRevolver = true;
                    break;
                case GunType.Laser:
                    GameManager.Instance.GameData.Laser = true;
                    break;
                case GunType.Shotgun:
                    GameManager.Instance.GameData.Shotgun = true;
                    break;
            }
            GameManager.Instance.SaveData();
        }
        else
        {
            rpp.SetText($"{target.gunName} 구매에\n 실패하셨습니다.");
        }
    }

    public bool CheckHasPurchaseThisGun(GunSO target)
    {
        switch (target.myType)
        {
            case GunType.Revolver:
                return GameManager.Instance.GameData.openRevolver;
            case GunType.Laser:
                return GameManager.Instance.GameData.Laser;
            case GunType.Shotgun:
                return GameManager.Instance.GameData.Shotgun;
            default:
                return false;
        }
    }
}
