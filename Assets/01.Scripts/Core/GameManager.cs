using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private Transform _poolTrm;

    private GameData _gameData;
    public GameData GameData => _gameData;

    [SerializeField]
    private AudioClip _bgmClip;

    [SerializeField]
    private InputReader _inputReader;

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
        if (instance != null)
        {
            Debug.LogError($"{typeof(UIManager)} instance is already exist!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        _inputReader.DisablePlayer();
        string data = PlayerPrefs.GetString("GameData", string.Empty);
        //if (string.IsNullOrEmpty(data))
        //{
        //}
        //_gameData = JsonUtility.FromJson<GameData>(data);
            _gameData = new GameData();

        PoolManager poolManager = new PoolManager(_poolTrm);
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
        isGameEnd = true;
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
        _inputReader.EnablePlayer();
        isGameEnd = false;
        _curTime = gameTime;
        onGameStartTrigger?.Invoke();
    }

    public void GameEnd()
    {
        if (isGameEnd) return;
        isGameEnd = true;
        _curTime = 0.0f;

        _inputReader.DisablePlayer();
        _poolTrm.BroadcastMessage("GotoPool");
        UIManager.Instanace.ChangeScene(UIDefine.UIType.GameResult);
        SceneChange("Result");
        onGameEndTrigger?.Invoke();
    }
    public void SceneChange(string sceneName, Action callback = null)
    {
        StartCoroutine(SceneChangeCor(sceneName, callback));
    }
    private IEnumerator SceneChangeCor(string sceneName, Action callback = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => operation.isDone);
        callback?.Invoke();
    }
}
