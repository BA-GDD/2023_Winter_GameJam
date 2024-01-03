using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvnetTrigger : MonoBehaviour
{
    [SerializeField]
    private EnemyBrain _enemy;

    public void EndAnimationHandle()
    {
        _enemy.EndAnimation();
    }

    public void AnimationEventHandle()
    {
        _enemy.animationEvent?.Invoke();
    }
    public void AttackAnimationEvnetHandle()
    {
        _enemy.attack.Attack();
    }

    public void EnemyDieEventHandle()
    {
        EnemySpawner.Instance.DeSpawnEnemy(_enemy);
    }
}
