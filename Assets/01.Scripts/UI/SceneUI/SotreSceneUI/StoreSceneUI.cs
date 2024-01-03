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
            rpp.SetText($"{target.name} ���Ÿ�\n �����ϼ̽��ϴ�.");

            _hasPurchaseGunList.Add(target);
        }
        else
        {
            rpp.SetText($"{target.name} ���ſ�\n �����ϼ̽��ϴ�.");
        }
    }

    public bool CheckHasPurchaseThisGun(GunSO target)
    {
        return _hasPurchaseGunList.Contains(target);
    }
}
