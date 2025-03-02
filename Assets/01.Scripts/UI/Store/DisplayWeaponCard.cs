using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayWeaponCard : MonoBehaviour
{
    [Header("셋팅 값들")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _skillInfoText;
    [SerializeField] private Image _gunProfile;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private RectTransform _InfoTrm;

    [Header("참조 값들")]
    [SerializeField] private GameObject _unHasBlock;
    [SerializeField] private TextMeshProUGUI _alreadyHaveText;
    [SerializeField] private StoreSceneUI _storeSceneUI;
    [SerializeField] private GunSO _myInfo;

    private float _waitingTime = 0.3f;
    private float _currentTIme = 0;
    private bool _onPointer;

    private bool _isActiveStatusPanel_;
    private bool _isActveStatusPanel
    {
        get
        {
            return _isActiveStatusPanel_;
        }
        set
        {
            _isActiveStatusPanel_ = value;
            ActiveStatusInfoPanel(value);
        }
    }

    private bool _isHavenThisGun => _storeSceneUI.CheckHasPurchaseThisGun(_myInfo);

    private StatusInfoPanel _stInfoPanel;

    private void Start()
    {
        _nameText.text = _myInfo.gunName;
        _infoText.text = _myInfo.flavorText;
        _skillInfoText.text = _myInfo.skillText;
        _gunProfile.sprite = _myInfo.gunProfile;
        _priceText.text = $"X {_myInfo.priceValue}";

        _unHasBlock.SetActive(!_isHavenThisGun);
        _alreadyHaveText.gameObject.SetActive(_isHavenThisGun);
    }

    private void Update()
    {
        if (!_onPointer) return;

        if(_currentTIme >= _waitingTime)
        {
            _onPointer = false;
            _isActveStatusPanel = true;
        }
        _currentTIme += Time.deltaTime;
    }

    public void OnPointerInThisImg()
    {
        _onPointer = true;
    }

    public void OutPointerInThisImg()
    {
        _onPointer = false;
        _isActveStatusPanel = false;
        _currentTIme = 0;
    }

    public void ActiveStatusInfoPanel(bool isActive)
    {
        if(isActive)
        {
            _stInfoPanel = PoolManager.Instance.Pop(PoolingType.StatusInfoPanel) as StatusInfoPanel;
            _stInfoPanel.transform.SetParent(transform);
            _stInfoPanel.transform.localPosition = _InfoTrm.localPosition;
            _stInfoPanel.transform.localScale = Vector3.one;
            _stInfoPanel.SetUpPanel(_myInfo);
        }
        else if(_stInfoPanel != null)
        {
            PoolManager.Instance.Push(_stInfoPanel);
        }
    }

    public void PurchaseThisItem()
    {
        if (_isHavenThisGun) return;

        _storeSceneUI.PurchaseThisGun(_myInfo);

        _unHasBlock.SetActive(!_isHavenThisGun);
        _alreadyHaveText.gameObject.SetActive(_isHavenThisGun);
    }
}
