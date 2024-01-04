using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laser : Gun
{
    [SerializeField]
    private PlayerLaser _laserEffect;

    protected override void Update()
    {
        base.Update();

        if (!isSkillProcess)
        {
            return;
        }

        _laserEffect.transform.position = firePosition.transform.position;
        Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - _laserEffect.transform.position;

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (transform.parent.localScale.x * direction.x < 0f ? 180f : 0f);
        _laserEffect.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        feedbackPlayer.PlayFeedback();

        Physics2D.BoxCastAll(_laserEffect.transform.position + _laserEffect.transform.right * (transform.parent.localScale.x * direction.x < 0f ? -1f : 1f) * _laserEffect.shootParticle.main.startSizeY.constant * 0.5f, new Vector2(_laserEffect.shootParticle.main.startSizeY.constant, _laserEffect.shootParticle.main.startSizeX.constant), angle, direction, 0f, enemyLayerMask).ToList().ForEach(enemy =>
        {
            if (enemy.transform.TryGetComponent(out MobBrain brain) && !brain.IsDead)
            {
                brain.OnHitHandle();
            }
        });
    }

    public override void ShootProcess()
    {
        if (isSkillProcess)
        {
            return;
        }

        _laserEffect.SetToShootLaser();
        _laserEffect.shootParticle.Play();

        _laserEffect.transform.position = firePosition.transform.position;
        Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - _laserEffect.transform.position;

        direction.Normalize();

        _laserEffect.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (transform.parent.localScale.x * direction.x < 0f ? 180f : 0f), Vector3.forward);

        feedbackPlayer.PlayFeedback();

        Physics2D.RaycastAll(_laserEffect.transform.position, direction, _laserEffect.shootParticle.main.startSizeY.constant, enemyLayerMask).ToList().ForEach(enemy =>
        {
            if (enemy.transform.TryGetComponent(out MobBrain brain) && !brain.IsDead)
            {
                brain.OnHitHandle();
                KillEvnetHandle();
            }
        });
    }

    public override void Skill(bool occurSkill)
    {
        if (CanUseSkill())
        {
            _laserEffect.SetToSkillLaser();
            _laserEffect.rootParticle.Play();

            skillProcessCoroutine = StartCoroutine(SkillProcess());
        }
    }

    protected override IEnumerator SkillProcess()
    {
        isSkillProcess = true;

        yield return new WaitForSeconds(_laserEffect.rootParticle.main.startLifetime.constant);

        InitializeSkill();
    }

    protected override void InitializeSkill()
    {
        base.InitializeSkill();

        _laserEffect.rootParticle.Stop();
    }}
