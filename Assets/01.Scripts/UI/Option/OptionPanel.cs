using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : PanelBase
{
    [SerializeField] private GameObject _blockContent;
    [Header("�⺻ ��")]
    [SerializeField] private float _mainNormalValue;
    [SerializeField] private float _bgmNormalValue;
    [SerializeField] private float _sfxNormalValue;

    public override void SetUpPanel()
    {
        // ���� �� �޾ƿ���
        //HandleSetMainSoundValue(������ ��);
        //HandleSetBGMSoundValue(������ ��);
        //HandleSetSFXSoundValue(������ ��);

        _blackPanelEndCallback += () => Destroy(gameObject);
        ActiveBlackPanel(true);
    }

    public override void InitPanel()
    {
        _blockContent.SetActive(false);
        ActiveBlackPanel(false);
    }

    public void ResetSoundValue()
    {
        HandleSetMainSoundValue(_mainNormalValue);
        HandleSetBGMSoundValue(_bgmNormalValue);
        HandleSetSFXSoundValue(_sfxNormalValue);
    }

    public void HandleSetMainSoundValue(float value)
    {

    }

    public void HandleSetBGMSoundValue(float value)
    {

    }

    public void HandleSetSFXSoundValue(float value)
    {

    }
}
