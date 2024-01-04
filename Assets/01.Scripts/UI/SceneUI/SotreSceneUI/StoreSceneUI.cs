using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSceneUI : SceneUIBase
{
    [SerializeField] private ResultOfPurchasePanel _resultPanel;

    public override void SetUp()
    {
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
            rpp.SetText($"{target.name} 구매를\n 성공하셨습니다.");

            switch (target.myType)
            {
                case GunType.Revolver:
                    GameManager.Instance.GameData.openRevolver = true;
                    break;
                case GunType.Razer:
                    GameManager.Instance.GameData.Razer = true;
                    break;
                case GunType.Shotgun:
                    GameManager.Instance.GameData.Shotgun = true;
                    break;
            }
            
        }
        else
        {
            rpp.SetText($"{target.name} 구매에\n 실패하셨습니다.");
        }
    }

    public bool CheckHasPurchaseThisGun(GunSO target)
    {
        switch (target.myType)
        {
            case GunType.Revolver:
                return GameManager.Instance.GameData.openRevolver;
            case GunType.Razer:
                return GameManager.Instance.GameData.Razer;
            case GunType.Shotgun:
                return GameManager.Instance.GameData.Shotgun;
            default:
                return false;
        }
    }
}
