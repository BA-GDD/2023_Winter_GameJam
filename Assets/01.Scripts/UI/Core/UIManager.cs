using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private ChangeSceneFade _sceneFade;
    private static UIManager _instance;
    public static UIManager Instanace
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<UIManager>();
            if (_instance == null)
            {
                Debug.LogError("Not Exist UIManager");
            }
            return _instance;
        }
    }

    private Canvas _canvas;
    public Transform CanvasTrm => _canvas.transform;

    [SerializeField] private UIType _startUIType;
    [SerializeField] private SceneUIBase[] _sceneUIGroup;
    private Dictionary<UIType, SceneUIBase> _uiSelecter = new Dictionary<UIType, SceneUIBase>();

    private SceneUIBase _currentScene;

    private Transform _sceneUITrm;
    private Transform _panelTrm;
    private PanelBase _onActivePanel;

    public UIType currentUIType;

    public Action EveryButtonHoverCallback;
    public Action EveryButtonClickCallback;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"{typeof(UIManager)} instance is already exist!");
            Destroy(gameObject);
            return;
        }

        _canvas = transform.Find("UICANVAS").GetComponent<Canvas>();
        _sceneUITrm = CanvasTrm.Find("SceneUIParent").transform;
        _panelTrm = CanvasTrm.Find("PanelParent").transform;

        foreach(SceneUIBase sceneObj in _sceneUIGroup)
        {
            _uiSelecter.Add(sceneObj.myUIType, sceneObj);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        ChangeSceneFade(_startUIType, false);
    }

    private void SceneChangeSystem(UIType toChangeScene)
    {
        if (_currentScene != null)
        {
            _currentScene.Init();
            Destroy(_currentScene.gameObject);
        }

        _currentScene = Instantiate(_uiSelecter[toChangeScene], _sceneUITrm);
        Debug.Log(_currentScene);
        currentUIType = toChangeScene;
        _currentScene.name = _currentScene.name.Replace("(Clone)", "");
        _currentScene.SetUp();

        _canvas.worldCamera = Camera.main;
        ButtonGenerate();
    }

    public void ChangeSceneFade(UIType toChangeScene, bool isFadeScene)
    {
        if(isFadeScene)
        {
            _sceneFade.FadeStart(() => SceneChangeSystem(toChangeScene));
            return;
        }

        SceneChangeSystem(toChangeScene);
    }

    private void ButtonGenerate()
    {
        Button[] btns = FindObjectsOfType<Button>();

        foreach (Button b in btns)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() => EveryButtonClickCallback?.Invoke());
        }
    }

    public void CreatePanel(PanelBase toCreatePanel)
    {
        _onActivePanel = Instantiate(toCreatePanel, _panelTrm);
        _onActivePanel.SetUpPanel();

        foreach (Button b in _onActivePanel.FindButtonInPanel())
        {
            b.onClick.AddListener(() => EveryButtonClickCallback?.Invoke());
        }
    }

    public PanelBase CreatePanel(PanelBase toCreatePanel, bool isSetUp)
    {
        _onActivePanel = Instantiate(toCreatePanel, _panelTrm);

        if (!isSetUp) return _onActivePanel;

        _onActivePanel.SetUpPanel();

        return _onActivePanel;
    }
}
