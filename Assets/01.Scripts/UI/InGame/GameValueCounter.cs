using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameValueCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _occupyText;
    [SerializeField] private Image _occValue;

    private float _currentOccValue;

    private float _currentTime;
    private float CurrentTime
    {
        get
        {
            return _currentTime;
        }
        set
        {
            _currentTime = value;
            if(value <= 10)
            {
                _timerText.color = Color.red;
                _timerText.text = $"{Mathf.FloorToInt(CurrentTime / 60)} : {CurrentTime % 60}";
            }
            else
            {
                _timerText.text = $"{Mathf.FloorToInt(CurrentTime / 60)} : {Mathf.FloorToInt(CurrentTime % 60)}";
            }
        }
    }

    private void Update()
    {
        CurrentTime = GameManager.Instance.CurrentTime;
        _scoreText.text = GameManager.Instance.Score.ToString();
        float amount = 0.0f;
        if (MapManager.Instance != null)
        {
             amount = Mathf.Lerp(0, 100, MapManager.Instance.WaterFillAmount());
            _currentOccValue = amount / 100;
            _occValue.fillAmount = Mathf.Lerp(_occValue.fillAmount, _currentOccValue, Time.deltaTime * 4);
        }
        _occupyText.text = $"{Mathf.FloorToInt(amount)}%";
    }
}
