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
        _milkCountText.text = GameManager.Instance.GameDataInstance.milkCount.ToString();
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
        if (GameManager.Instance.GameDataInstance.milkCount >= target.priceValue)
        {
            GameManager.Instance.GameDataInstance.milkCount -= target.priceValue;
            rpp.SetText($"{target.gunName} 구매를\n 성공하셨습니다.");

            switch (target.myType)
            {
                case GunType.Revolver:
                    GameManager.Instance.GameDataInstance.openRevolver = true;
                    break;
                case GunType.Laser:
                    GameManager.Instance.GameDataInstance.openLaser = true;
                    break;
                case GunType.Shotgun:
                    GameManager.Instance.GameDataInstance.openShotgun = true;
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
                return GameManager.Instance.GameDataInstance.openRevolver;
            case GunType.Laser:
                return GameManager.Instance.GameDataInstance.openLaser;
            case GunType.Shotgun:
                return GameManager.Instance.GameDataInstance.openShotgun;
            default:
                return false;
        }
    }
}
