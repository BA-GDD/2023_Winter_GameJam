using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameValueCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _occupyText;

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
        _occupyText.text = Mathf.FloorToInt(GameManager.Instance.occupationPercent).ToString();
    }
}
