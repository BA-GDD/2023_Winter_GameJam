using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        _player.DeleteWaterGaugeHandle(_onsenWater);
        _player.UnequipGun();
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GameExitPanel exp = UIManager.Instanace.CreatePanel(_exitPanel, false) as GameExitPanel;
            exp.SetText("���� �κ�� ����<br>�Ͻðڽ��ϱ�?");
            exp.SetUpPanel();
        }
    }
}
