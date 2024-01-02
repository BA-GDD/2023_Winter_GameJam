using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private const string beforeTimeKey = "before";
    private const string bestTimekey = "best";

    public float beforeTime;
    public float bestTime;

    public GameData()
    {
        beforeTime = PlayerPrefs.GetFloat(beforeTimeKey, 0);
        bestTime = PlayerPrefs.GetFloat(bestTimekey, 0);
    }

    public void Save(float time)
    {
        beforeTime = time;
        PlayerPrefs.SetFloat(beforeTimeKey, beforeTime);
        if (bestTime > time)
        {
            bestTime = time;
            PlayerPrefs.SetFloat(bestTimekey, bestTime);
        }
    }
}
