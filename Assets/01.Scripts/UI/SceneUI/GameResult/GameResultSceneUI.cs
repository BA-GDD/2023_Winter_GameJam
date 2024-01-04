using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameResultSceneUI : SceneUIBase
{
    [SerializeField] private UnityEvent<float, float, float> _scoreSetEvent;
    [SerializeField] private UnityEvent<int> _milkThrowEvent;
    private GameObject _remainFX;
    public bool isEndTimeLine;

    [SerializeField] private AudioClip _bgmClip;

    public void GoToLobby()
    {
        GameManager.Instance.GameData.SetTime(GameManager.Instance.Score);
        GameManager.Instance.GameData.Save();
        UIManager.Instanace.ChangeSceneFade(UIDefine.UIType.Lobby, true);
        //UIManager.Instanace.ChangeScene(UIDefine.UIType.Lobby);
    }

    private void Update()
    {
        if (!isEndTimeLine) return;

        if(Input.anyKeyDown)
        {
            GoToLobby();
        }
    }

    private void Start()
    {
        //SetUp();
        //SoundManager.Instance.Stop("BGM");
    }

    public override void SetUp()
    {
        _milkThrowEvent?.Invoke(Mathf.FloorToInt(GameManager.Instance.Score));
        _scoreSetEvent?.Invoke(GameManager.Instance.Score, 
                               GameManager.Instance.GameData.beforeTime,
                               GameManager.Instance.GameData.bestTime);
        SoundManager.Instance.Play(_bgmClip,1,1,1,true,"GameResult");
    }

    public override void Init()
    {
        SoundManager.Instance.Stop("GameResult");
        Destroy(_remainFX);
    }
}
