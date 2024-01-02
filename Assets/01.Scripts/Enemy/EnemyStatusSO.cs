using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Revolver, // 중거리
    Sniper, // 저격
    Shield, // 방패
    Melee, // 근접
}

[CreateAssetMenu(menuName = "SO/EnemyStatus")]
public class EnemyStatusSO : ScriptableObject
{
    public float moveSpeed;
    public float atkRange;
    public float atkDelay;
    public Sprite sprite;
    public EnemyType type;
}
