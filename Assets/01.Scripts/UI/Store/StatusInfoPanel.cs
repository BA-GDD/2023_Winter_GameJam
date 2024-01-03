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
        _shootDelayText.text = $"���ݼӵ� : {target.shootDelay}";
        _maxCapacityText.text = $"�� �뷮 : {target.maximumCapacity}";
        _fillCapSecText.text = $"���� �ӵ� : {target.fillCapacityPerSecond}";
        _useCapShotText.text = $"��� �� �� : {target.useCapacityPerShoot}";
    }
}
