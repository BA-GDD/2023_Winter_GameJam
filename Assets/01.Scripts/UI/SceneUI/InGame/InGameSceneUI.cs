using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneUI : SceneUIBase
{
    private Player _player;
    private OnsenWaterGage _onsenWater;
    [SerializeField] private GameExitPanel _exitPanel;

    public override void SetUp()
    {
        _player = FindObjectOfType<Player>();

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
            GameExitPanel exp = UIManager.Instanace.CreatePanel(_exitPanel, false) as GameExitPanel;
            exp.SetText("���� �κ�� ����<br>�Ͻðڽ��ϱ�?");
            exp.SetUpPanel();
        }
    }
}
