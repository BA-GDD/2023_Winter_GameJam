using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusInfoPanel : PoolableMono
{
    [SerializeField] private TextMeshProUGUI _shootDelayText;
    [SerializeField] private TextMeshProUGUI _maxCapacityText;
    [SerializeField] private TextMeshProUGUI _fillCapSecText;
    [SerializeField] private TextMeshProUGUI _useCapShotText;

    public override void Init()
    {
    }

    public void SetUpPanel(GunSO target)
    {
        _shootDelayText.text = $"공격속도 : {target.shootDelay}";
        _maxCapacityText.text = $"물 용량 : {target.maximumCapacity}";
        _fillCapSecText.text = $"장전 속도 : {target.fillCapacityPerSecond}";
        _useCapShotText.text = $"쏘는 물 양 : {target.useCapacityPerShoot}";
    }
}
