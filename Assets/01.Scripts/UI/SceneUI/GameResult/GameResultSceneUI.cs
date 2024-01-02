using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameResultSceneUI : SceneUIBase
{
    [SerializeField] private GameObject _partyFX;
    [SerializeField] private UnityEvent<float, float, float> _scoreSetEvent;
    private GameObject _remainFX;
    public bool isEndTimeLine;

    public void GoToLobby()
    {
        UIManager.Instanace.ChangeScene(UIDefine.UIType.Lobby);
    }

    private void Update()
    {
        if (!isEndTimeLine) return;

        if(Input.anyKeyDown)
        {
            GoToLobby();
        }
    }

    public override void SetUp()
    {
        //_scoreSetEvent?.Invoke();
        _remainFX = Instantiate(_partyFX);
    }

    public override void Init()
    {
        Destroy(_remainFX);
    }
}
