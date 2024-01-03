using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector]
    public Camera mainCamera;

    [Header("���� �̺�Ʈ")]
    public UnityEvent onGameEndTrigger;
    public UnityEvent onGameStartTrigger;
    public UnityEvent onScoreChanged;

    [Header("���ӿ� �ʿ��� ��ġ")]
    public float gameTime = 5.0f; //�ʴ���
    private float _curTime = 5.0f; //�ʴ���
    public float CurrentTime => _curTime;

    [Range(0f, 100f)]
    public float occupationPercent = 0.0f; //0~100����
    public bool isGameEnd = false;

    public GameData gameData;

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
        gameData = new GameData();
        PoolManager poolManager = new PoolManager(transform);
        foreach(var item in _poolList.poolList)
        {
            poolManager.CreatePool(item.prefab, item.type, item.count);
        }
        PoolManager.Instance = poolManager;
        mainCamera = Camera.main;
        _gameData = new GameData();
    }

    private void Start()
    {
        player = FindFirstObjectByType<Player>().transform;
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
