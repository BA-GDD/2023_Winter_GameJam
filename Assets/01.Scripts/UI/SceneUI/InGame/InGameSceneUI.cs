using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneUI : SceneUIBase
{
    private Player _player;
    private OnsenWaterGage _onsenWater;
    [SerializeField] private OptionPanel _optionPanel;

    public override void SetUp()
    {
        _player = FindObjectOfType<Player>();
        _player.EquipGun(GameManager.Instance.selectGunType);

        _onsenWater = transform.Find("OnsenWaterGage").GetComponent<OnsenWaterGage>();
        _player.SetWaterGaugeHandle(_onsenWater);
    }
    public override void Init()
    {
        _player.UnequipGun();
        _player.DeleteWaterGaugeHandle(_onsenWater);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instanace.CreatePanel(_optionPanel);
        }
    }
}
