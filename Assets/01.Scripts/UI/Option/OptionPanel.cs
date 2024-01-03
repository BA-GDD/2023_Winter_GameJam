using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : PanelBase
{
    [SerializeField] private GameObject _blockContent;
    [Header("기본 값")]
    [SerializeField] private float _mainNormalValue;
    [SerializeField] private float _bgmNormalValue;
    [SerializeField] private float _sfxNormalValue;

    [Header("슬라이더")]
    [SerializeField] private Slider _mainVolumeSlider;
    [SerializeField] private Slider _bgmVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    public override void SetUpPanel()
    {
        // 사운드 값 받아오기
        _mainVolumeSlider.value = SoundManager.masterVolume;
        _bgmVolumeSlider.value = SoundManager.bgmVolume;
        _sfxVolumeSlider.value = SoundManager.sfxVolume;

        _blackPanelEndCallback += () => Destroy(gameObject);
        ActiveBlackPanel(true);

        Time.timeScale = 0;
    }

    public override void InitPanel()
    {
        Time.timeScale = 1;
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
        SoundManager.masterVolume = value;
    }

    public void HandleSetBGMSoundValue(float value)
    {
        SoundManager.bgmVolume = value;
    }

    public void HandleSetSFXSoundValue(float value)
    {
        SoundManager.sfxVolume = value;
    }
}
