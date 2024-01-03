using System;
using UnityEngine;

[Serializable]
public abstract class Feedback: MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void CompleteFeedback();
}
