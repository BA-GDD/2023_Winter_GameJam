using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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

    private Transform _canvasTrm;
    public Transform CanvasTrm => _canvasTrm;

    [SerializeField] private UIType _startUIType;
    [SerializeField] private SceneUIBase[] _sceneUIGroup;
    private Dictionary<UIType, SceneUIBase> _uiSelecter = new Dictionary<UIType, SceneUIBase>();

    private SceneUIBase _currentScene;

    private Transform _sceneUITrm;
    private Transform _panelTrm;
    private PanelBase _onActivePanel;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"{typeof(UIManager)} instance is already exist!");
            Destroy(gameObject);
            return;
        }

        _canvasTrm = transform.Find("UICANVAS").transform;
        _sceneUITrm = _canvasTrm.Find("SceneUIParent").transform;
        _panelTrm = _canvasTrm.Find("PanelParent").transform;

        foreach(SceneUIBase sceneObj in _sceneUIGroup)
        {
            _uiSelecter.Add(sceneObj.myUIType, sceneObj);
        }
    }

    private void Start()
    {
        //ChangeScene(_startUIType);
    }

    public void ChangeScene(UIType toChangeScene)
    {
        if(_currentScene != null)
        {
            Destroy(_currentScene.gameObject);
        }

        _currentScene = Instantiate(_uiSelecter[toChangeScene], _sceneUITrm);
        _currentScene.SetUp();
    }

    public void CreatePanel(PanelBase toCreatePanel)
    {
        _onActivePanel = Instantiate(toCreatePanel, _panelTrm);
        _onActivePanel.SetUpPanel();
    }
}
