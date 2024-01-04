using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float beforeTime;
    public float bestTime;
    public int milkCoount;
    public bool openRevolver = true;
    public bool Razer;
    public bool Shotgun;
    public bool isLookPrologue;

    public void SetTime(float time)
    {
        beforeTime = time;
        if (bestTime > time)
        {
            bestTime = time;
        }
    }
    public void Save()
    {
        PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(this));
    }
}   
