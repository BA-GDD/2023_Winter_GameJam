using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("게임 시작 및 끝 이벤트")]
    public UnityEvent onGameEndTrigger;
    public UnityEvent onGameStartTrigger;

    [Header("게임에 필요한 수치")]
    public float gameTime = 5.0f; //초단위
    private float _curTime = 5.0f; //초단위
    [Range(0f, 100f)]
    public float occupationPercent = 0.0f; //0~100까지
    public bool isGameEnd = false;

    [ContextMenu("is i have instance?")]
    public void IsIHaveInstance()
    {
        print(Instance != null ? "yes" : "no");
    }

    private void Update()
    {
        if(!isGameEnd)
            _curTime -= Time.deltaTime;

        if (_curTime <= 0.0f)
        {
            GameEnd();
        }
    }

    public void GameStart()
    {
        isGameEnd = false;
        _curTime = gameTime;

        onGameStartTrigger?.Invoke();
    }

    public void GameEnd()
    {
        isGameEnd = true;
        _curTime = 0.0f;

        onGameEndTrigger?.Invoke();
    }
}
