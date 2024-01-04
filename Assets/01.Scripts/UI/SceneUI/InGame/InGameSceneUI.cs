using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameSceneUI : SceneUIBase
{
    private Player _player;
    [HideInInspector]
    public OnsenWaterGage onsenWater;
    [HideInInspector]
    public SkillBarGroup skillBarGroup;
    [SerializeField] private GameExitPanel _exitPanel;
    [SerializeField] private AudioClip _bgmClip;

    public override void SetUp()
    {
        _player = FindObjectOfType<Player>();
        _player.inGameSceneUI = this;


        onsenWater = transform.Find("GamePanel/OnsenWaterGage").GetComponent<OnsenWaterGage>();
        skillBarGroup = transform.Find("SkillBarGroup").GetComponent<SkillBarGroup>();

        _player.SetWaterGaugeHandle(onsenWater);
        _player.SetSkillGroup(skillBarGroup);
        SoundManager.Instance.Play(GameManager.Instance.bgmClip, 0.3f, 1f, 1, true, "BGM");
    }
    public override void Init()
    {
        _player.DeleteSkillGroup(_skillBarGroup); 
        _player.DeleteWaterGaugeHandle(_onsenWater);
        _player.UnequipGun();
        SoundManager.Instance.Stop("InGame");

        UIManager.Instanace._canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        UIManager.Instanace._canvas.worldCamera = Camera.main;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GameExitPanel exp = UIManager.Instanace.CreatePanel(_exitPanel, false) as GameExitPanel;
            exp.SetText("���� �κ�� ����<br>�Ͻðڽ��ϱ�?");
            exp.SetUpPanel();
        }
    }
}
