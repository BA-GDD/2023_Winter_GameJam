using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public Transform player
    {
        get
        {
            return FindObjectOfType<Player>().transform;
        }
    }
    public Camera mainCamera;
    public GunType selectGunType;

    [Range(0f, 1f)]
    public float occupationPercent = 0.0f; //0~100����
    public bool isGameEnd = false;

    [SerializeField]
    private PoolListSO _poolList;

    private GameData _gameData;
    public GameData GameData => _gameData;

    [SerializeField]
    private AudioClip _bgmClip;

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

    private readonly string _filePath = Path.Combine(Application.dataPath, "GameData.json");
    private string _jsonData;

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(_gameData);
        File.WriteAllText(_filePath, jsonData);
    }

    private void Awake()
    {
        if(File.Exists(_filePath))
        {
            _jsonData = File.ReadAllText(_filePath);
            _gameData = JsonUtility.FromJson<GameData>(_jsonData);
        }
        else
        {
            _gameData = new GameData();
            SaveData();
        }

        PoolManager poolManager = new PoolManager(transform);
        foreach (var item in _poolList.poolList)
        {
            poolManager.CreatePool(item.prefab, item.type, item.count);
        }
        PoolManager.Instance = poolManager;
        mainCamera = Camera.main;

        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        SoundManager.Instance.Play(_bgmClip, 0.3f, 1, 1, true);

    }

    private void Update()
    {
        if (!isGameEnd)
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
    public void SceneChange(string sceneName, Action callback)
    {
        StartCoroutine(SceneChangeCor(sceneName, callback));
    }
    private IEnumerator SceneChangeCor(string sceneName, Action callback)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => operation.isDone);
        callback?.Invoke();
    }
}
