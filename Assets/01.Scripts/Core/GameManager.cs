using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

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
    public GameData GameDataInstance
    {
        get => _gameData;
        set => _gameData = value;
    }

    public AudioClip hawon;

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

    private readonly string _filePath = Path.Combine(Application.dataPath, "GameData.json");
    private string _jsonData;

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(_gameData);
        File.WriteAllText(_filePath, jsonData);
    }

    private void Awake()
    {
        if (File.Exists(_filePath))
        {
            _jsonData = File.ReadAllText(_filePath);
            _gameData = JsonUtility.FromJson<GameData>(_jsonData);
        }
        else
        {
            _gameData = new GameData();
            SaveData();
        }
        if (instance != null)
        {
            Debug.LogError($"{typeof(GameManager)} instance is already exist!");
            Destroy(gameObject);
        }
        instance = this;

        PoolManager poolManager = new PoolManager(_poolTrm);
        foreach (var item in _poolList.poolList)
        {
            poolManager.CreatePool(item.prefab, item.type, item.count);
        }
        PoolManager.Instance = poolManager;
        mainCamera = Camera.main;
        //SoundManager.Instance.Play(_bgmClip, 0.3f, 1, 1, true);

        selectGunType = (GunType)Enum.Parse(typeof(GunType), _gameData.equipedGun);

        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        //player = FindObjectOfType<Player>().transform;
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
        //if (_curTime <= 0.0f || MapManager.Instance.WaterFillAmount() > occupationPercent)
        //{
        //    GameEnd();
        //}]
        if (MapManager.Instance != null)
            Score = ((int)((gameTime - _curTime) * (int)(MapManager.Instance.WaterFillAmount() * 100.0f)) + ((int)MapManager.Instance.WaterFillAmount() * 100.0f)) * 0.1f;

        // Develper Code
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            GameDataInstance.milkCount = 100000;

            SaveData();
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

        StartCoroutine(GameEndAnimation());

        onGameEndTrigger?.Invoke();
    }

    public IEnumerator GameEndAnimation()
    {
        Time.timeScale = 0.3f;

        player.transform.Find("end").GetComponent<PlayableDirector>().Play();
        yield return new WaitForSecondsRealtime(1f);
        SoundManager.Instance.Play(hawon, 1, 1, 2);
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1.0f;

        UIManager.Instanace.ChangeSceneFade(UIDefine.UIType.GameResult, true);
        SceneChange("Result");
    }

    public void SceneChange(string sceneName, Action callback = null)
    {
        StartCoroutine(SceneChangeCor(sceneName, callback));
    }
    private IEnumerator SceneChangeCor(string sceneName, Action callback = null)
    {
        UIManager.Instanace._canvas.worldCamera = Camera.main;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => operation.isDone);
        callback?.Invoke();
    }
}
