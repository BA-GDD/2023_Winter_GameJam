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

    public override void SetUp()
    {
        _player = FindObjectOfType<Player>();
        _player.inGameSceneUI = this;

        onsenWater = transform.Find("OnsenWaterGage").GetComponent<OnsenWaterGage>();
        skillBarGroup = transform.Find("SkillBarGroup").GetComponent<SkillBarGroup>();
        _player.SetWaterGaugeHandle(onsenWater);
        _player.SetSkillGroup(skillBarGroup);
        SoundManager.Instance.Play(GameManager.Instance.bgmClip, 0.3f, 1f, 1, true, "BGM");
    }
    public override void Init()
    {
        UIManager.Instanace._canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        UIManager.Instanace._canvas.worldCamera = Camera.main;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GameExitPanel exp = UIManager.Instanace.CreatePanel(_exitPanel, false) as GameExitPanel;
            exp.SetText("정말 로비로 퇴장<br>하시겠습니까?");
            exp.SetUpPanel();
        }
    }
}
