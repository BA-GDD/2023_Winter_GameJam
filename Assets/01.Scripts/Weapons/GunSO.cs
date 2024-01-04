using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Gun")]
public class GunSO : ScriptableObject
{
    [Header("Features")]
    public GunType myType;
    public string gunName;
    public Sprite gunProfile;
    [Multiline(5)]
    public string flavorText;
    [Multiline(3)]
    public string skillText;
    public Animator gunAnimator;

    [Header("Statistics")]
    public float shootDelay;
    public float maximumCapacity;
    public float fillCapacityPerSecond;
    public float useCapacityPerShoot;
    public float requireSkillGauge;
    public float fillSkillGauge;
    public int priceValue;
}
