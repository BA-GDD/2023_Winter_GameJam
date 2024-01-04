using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string equipedGun = "Revolver";
    public float beforeScore;
    public float bestScore;
    public int milkCount;
    public bool openRevolver = true;
    public bool openLaser;
    public bool openShotgun;
    public bool isLookPrologue;

    public void SetScore(float score)
    {
        beforeScore = score;

        if (bestScore < score)
        {
            bestScore = score;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(this));
    }
}
