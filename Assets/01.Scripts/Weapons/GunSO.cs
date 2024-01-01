using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Gun")]
public class GunSO : ScriptableObject
{
    public string gunName;
    public string flavorText;
    public Animator gunAnimator;
    public float shootDelay;
    public float maximumCapacity;
    public float fillCapacityPerSecond;
    public float useCapacityPerShoot;
}
