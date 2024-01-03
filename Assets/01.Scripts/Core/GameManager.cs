using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("게임 이벤트")]
    public UnityEvent onGameEndTrigger;
    public UnityEvent onGameStartTrigger;
    public UnityEvent onScoreChanged;

    [Header("게임에 필요한 수치")]
    public float gameTime = 5.0f; //초단위
    private float _curTime = 5.0f; //초단위
    public float CurrentTime => _curTime;
    public Transform player { get; set; }
    public Camera mainCamera;
    public GunType selectGunType;

    [Range(0f, 100f)]
    public float occupationPercent = 0.0f; //0~100까지
    public bool isGameEnd = false;

    private GameData _gameData;
    public GameData GameData => _gameData;

    private float _score;
    public float Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            onScoreChanged?.Invoke();
        }
    }

    private void Awake()
    {
        _gameData = new GameData();
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
