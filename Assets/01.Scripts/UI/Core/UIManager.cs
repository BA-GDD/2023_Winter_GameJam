using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using System;
using UnityEngine.UI;

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

    [SerializeField] private SceneUI[] _sceneUIGroup;
    private Dictionary<UIType, SceneUI> _uiSelecter = new Dictionary<UIType, SceneUI>();

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"{typeof(UIManager)} instance is already exist!");
            Destroy(gameObject);
            return;
        }

        _canvasTrm = GameObject.Find("UICANVAS").transform;

        foreach(SceneUI sceneObj in _sceneUIGroup)
        {
            _uiSelecter.Add(sceneObj.myUIType, sceneObj);
        }
    }
}
