using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
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

    [SerializeField] private PoolListSO _list;

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

        PoolManager pm = new PoolManager(CanvasTrm);

        foreach(PoolingItem p in _list.poolList)
        {
            pm.CreatePool(p.prefab, p.type, p.count);
        }
        
        DontDestroyOnLoad(this.gameObject);
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
        _currentScene.name = _currentScene.name.Replace("(Clone)", "");
        _currentScene.SetUp();
    }

    public void CreatePanel(PanelBase toCreatePanel)
    {
        _onActivePanel = Instantiate(toCreatePanel, _panelTrm);
        _onActivePanel.SetUpPanel();
    }

    public PanelBase CreatePanel(PanelBase toCreatePanel, bool isSetUp)
    {
        _onActivePanel = Instantiate(toCreatePanel, _panelTrm);

        if (!isSetUp) return _onActivePanel;

        _onActivePanel.SetUpPanel();

        return _onActivePanel;
    }
}
