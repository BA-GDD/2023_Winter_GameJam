using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameSceneUI : SceneUIBase
{
    private Player _player;
    private OnsenWaterGage _onsenWater;
    private SkillBarGroup _skillBarGroup;
    [SerializeField] private GameExitPanel _exitPanel;
    [SerializeField] private AudioClip _bgmClip;

    public override void SetUp()
    {
        _player = FindObjectOfType<Player>();

        _onsenWater = transform.Find("OnsenWaterGage").GetComponent<OnsenWaterGage>();
        _skillBarGroup = transform.Find("SkillBarGroup").GetComponent<SkillBarGroup>();
        _player.SetWaterGaugeHandle(_onsenWater);
        _player.SetSkillGroup(_skillBarGroup);
        SoundManager.Instance.Play(_bgmClip, 0.3f, 1f, 1, true, "InGame");
    }
    public override void Init()
    {
        _player.DeleteSkillGroup(_skillBarGroup);
        _player.DeleteWaterGaugeHandle(_onsenWater);
        _player.UnequipGun();
        SoundManager.Instance.Stop("InGame");
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GameExitPanel exp = UIManager.Instanace.CreatePanel(_exitPanel, false) as GameExitPanel;
            exp.SetText("정말 로비로 퇴장<br>하시겠습니까?");
            exp.SetUpPanel();
        }
    }
}
