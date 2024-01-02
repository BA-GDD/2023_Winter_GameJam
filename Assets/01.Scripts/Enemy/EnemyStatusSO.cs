using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Revolver, // �߰Ÿ�
    Sniper, // ����
    Shield, // ����
    Melee, // ����
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
