using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSceneUI : SceneUIBase
{
    private int _haveMilks;
    private List<GunSO> _hasPurchaseGunList = new List<GunSO>();
    [SerializeField] private ResultOfPurchasePanel _resultPanel;

    public override void SetUp()
    {
    }

    public override void Init()
    {

    }

    public void ExitToLobby()
    {
        UIManager.Instanace.ChangeScene(UIDefine.UIType.Lobby);
    }

    public void PurchaseThisGun(GunSO target)
    {
        ResultOfPurchasePanel rpp = (ResultOfPurchasePanel)UIManager.Instanace.CreatePanel(_resultPanel, true);
        if (_haveMilks >= target.priceValue)
        {
            _haveMilks -= target.priceValue;
            rpp.SetText($"{target.name} 구매를\n 성공하셨습니다.");

            _hasPurchaseGunList.Add(target);
        }
        else
        {
            rpp.SetText($"{target.name} 구매에\n 실패하셨습니다.");
        }
    }

    public bool CheckHasPurchaseThisGun(GunSO target)
    {
        return _hasPurchaseGunList.Contains(target);
    }
}
