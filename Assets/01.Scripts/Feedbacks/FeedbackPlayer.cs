using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField] private List<Feedback> _feedbackList = new List<Feedback>();

    private void Awake()
    {
        //GetComponents<Feedback>(_feedbackList);
    }

    public void PlayFeedback()
    {
        foreach(Feedback feedback in _feedbackList)
        {
            feedback.CompleteFeedback();
            feedback.CreateFeedback();
        }
    }

    #region Debug
    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            PlayFeedback();
        }
    }
    #endregion
}
