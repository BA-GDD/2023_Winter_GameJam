using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    public Camera mainCamera;

    [Header("게임 이벤트")]
    public UnityEvent onGameEndTrigger;
    public UnityEvent onGameStartTrigger;
    public UnityEvent onScoreChanged;

    [Header("게임에 필요한 수치")]
    public float gameTime = 5.0f; //초단위
    private float _curTime = 5.0f; //초단위
    public float CurrentTime => _curTime;

    [Range(0f, 100f)]
    public float occupationPercent = 0.0f; //0~100까지
    public bool isGameEnd = false;

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

    public Transform player { get; private set; }

    [SerializeField]
    private PoolListSO _poolList;

    private void Awake()
    {
        PoolManager poolManager = new PoolManager(transform);
        foreach(var item in _poolList.poolList)
        {
            poolManager.CreatePool(item.prefab, item.type, item.count);
        }
        PoolManager.Instance = poolManager;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
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
