using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Melee,
    Revolver,
    Sniper,
    Shield,
}

[CreateAssetMenu(menuName = "SO/EnemyStatus")]
public class EnemyStatusSO : ScriptableObject
{
    public float moveSpeed;
    public float atkRange;
    public float atkDelay;
    public Animator animator;
    public EnemyType type;
}
