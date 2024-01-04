using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Revolver : Gun
{
    private readonly int _isOutlineHash = Shader.PropertyToID("_isOutline");
    [SerializeField]
    private Transform _skillRangeCircle;
    [SerializeField]
    private float _rangeCircleExpandSpeed;
    [SerializeField]
    private float _rangeCircleMaxRadius;
    [SerializeField]
    private float _skillShootDelay;
    private List<Collider2D> _targets;
    private float _rangeCircleRadius;

    [SerializeField]
    private ParticleSystem _revolverSkillFX;
    [SerializeField]
    private FeedbackPlayer _feedbackPlayer;

    public override void ShootProcess()
    {
        if (isSkillProcess)
        {
            return;
        }

        PlayerBullet bullet = PoolManager.Instance.Pop(PoolingType.PlayerBullet) as PlayerBullet;
        bullet.transform.position = firePosition.position;
        Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - bullet.transform.position;
        bullet.ApeendEvent(KillEvnetHandle);

        direction.Normalize();

        bullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    public override void Skill(bool occurSkill)
    {
        if (CanUseSkill())
        {
            if (occurSkill)
            {
                Time.timeScale = 1f;
                _rangeCircleRadius = 0f;
                _skillRangeCircle.localScale = Vector2.zero;

                foreach (var target in _targets)
                {
                    if (target.TryGetComponent(out MobBrain brain))
                    {
                        brain._spriteRenderer.material.SetInt(_isOutlineHash, 0);
                    }
                    else
                    {
                        target.GetComponent<SpriteRenderer>().material.SetInt(_isOutlineHash, 0);
                    }
                }

                StartCoroutine(SkillProcess());
            }
            else
            {
                Time.timeScale = 0.2f;
                _rangeCircleRadius += _rangeCircleExpandSpeed * Time.unscaledDeltaTime;
                _rangeCircleRadius = Mathf.Clamp(_rangeCircleRadius, 0f, _rangeCircleMaxRadius);
                _skillRangeCircle.localScale = new Vector2(_rangeCircleRadius * 2f, _rangeCircleRadius * 2f);
                _targets = Physics2D.OverlapCircleAll(_skillRangeCircle.position, _rangeCircleRadius * 5f, enemyLayerMask).ToList();

                foreach (var target in _targets)
                {
                    if (target.TryGetComponent(out MobBrain brain))
                    {
                        brain._spriteRenderer.material.SetInt(_isOutlineHash, 1);
                    }
                    else
                    {
                        target.GetComponent<SpriteRenderer>().material.SetInt(_isOutlineHash, 1);
                    }
                }
            }
        }
    }

    private IEnumerator SkillProcess()
    {
        isSkillProcess = true;

        _revolverSkillFX.Play();
        _feedbackPlayer.PlayFeedback();
        foreach (var target in _targets)
        {
            if (!target.TryGetComponent(out MobBrain brain))
            {
                continue;
            }

            Transform gunSocket = transform.parent;
            Vector2 direction = target.transform.position - gunSocket.position;

            direction.Normalize();

            if (owner.transform.localScale.x * gunSocket.localScale.x * direction.x < 0f)
            {
                Flip();
            }

            gunSocket.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (direction.x < 0f ? 180f : 0f), Vector3.forward);

            Bullet bullet = PoolManager.Instance.Pop(PoolingType.PlayerBullet) as Bullet;
            bullet.isMissileMode = true;
            bullet.targetOfMissile = target;
            bullet.bulletSpeed = 20.0f;
            bullet.lifeTime = 10f;
            bullet.transform.position = firePosition.position;

            yield return new WaitForSeconds(_skillShootDelay);
        }

        isSkillProcess = false;

        _targets.Clear();
        base.Skill(true);
    }
}
