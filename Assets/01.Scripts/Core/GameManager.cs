using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("���� �̺�Ʈ")]
    public UnityEvent onGameEndTrigger;
    public UnityEvent onGameStartTrigger;
    public UnityEvent onScoreChanged;

    [Header("���ӿ� �ʿ��� ��ġ")]
    public float gameTime = 5.0f; //�ʴ���
    private float _curTime = 5.0f; //�ʴ���
    public float CurrentTime => _curTime;
    public Transform player { get; set; }
    public Camera mainCamera;
    public GunType selectGunType;

    [Range(0f, 100f)]
    public float occupationPercent = 0.0f; //0~100����
    public bool isGameEnd = false;

    [SerializeField]
    private PoolListSO _poolList;

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

    [SerializeField]
    private AudioClip _bgmClip;

    private void Awake()
    {
        _gameData = new GameData();
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
        player = FindObjectOfType<Player>().transform;
        SoundManager.Instance.Play(_bgmClip, 1, 1, 1);
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
