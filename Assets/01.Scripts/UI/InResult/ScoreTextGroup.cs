using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextGroup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _beforeScore;
    [SerializeField] private TextMeshProUGUI _bestScore;

    public void SetScoreText(float current, float before, float best)
    {
        _currentScore.text = current.ToString();
        _beforeScore.text = before.ToString();
        _bestScore.text = best.ToString();
    }
}
