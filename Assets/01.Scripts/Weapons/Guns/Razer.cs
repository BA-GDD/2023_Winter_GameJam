using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class Razer : Gun
{
    [SerializeField]
    private PlayerRazer _razerEffect;

    protected override void Update()
    {
        base.Update();

        if (!isSkillProcess)
        {
            return;
        }

        _razerEffect.transform.position = firePosition.transform.position;
        /*Vector2 */direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - _razerEffect.transform.position;

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (transform.parent.localScale.x * direction.x < 0f ? 180f : 0f);
        _razerEffect.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Physics2D.BoxCastAll(_razerEffect.transform.position + _razerEffect.transform.right * (transform.parent.localScale.x * direction.x < 0f ? -1f : 1f) * _razerEffect.particle.main.startSizeX.constant * 0.5f, new Vector2(_razerEffect.particle.main.startSizeX.constant, _razerEffect.particle.main.startSizeY.constant), angle, direction, 0f, enemyLayerMask).ToList().ForEach(enemy =>
        {
            if (enemy.transform.TryGetComponent(out MobBrain brain) && !brain.IsDead)
            {
                brain.OnHitHandle();
            }
        });
    }
    Vector2 direction;
    public override void ShootProcess()
    {
        if (isSkillProcess)
        {
            return;
        }

        _razerEffect.SetToShootRazer();
        _razerEffect.particle.Play();

        _razerEffect.transform.position = firePosition.transform.position;
        Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - _razerEffect.transform.position;

        direction.Normalize();

        _razerEffect.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (transform.parent.localScale.x * direction.x < 0f ? 180f : 0f), Vector3.forward);

        Physics2D.RaycastAll(_razerEffect.transform.position, direction, _razerEffect.particle.main.startSizeX.constant, enemyLayerMask).ToList().ForEach(enemy =>
        {
            if (enemy.transform.TryGetComponent(out MobBrain brain) && !brain.IsDead)
            {
                brain.OnHitHandle();
            }
        });
    }

    public override void Skill(bool occurSkill)
    {
        if (CanUseSkill()/*Debug*/ && !isSkillProcess)
        {
            _razerEffect.SetToSkillRazer();
            _razerEffect.particle.Play();
            StartCoroutine(SkillProcess());
            base.Skill(occurSkill);
        }
    }

    public IEnumerator SkillProcess()
    {
        isSkillProcess = true;

        yield return new WaitForSeconds(_razerEffect.particle.main.duration);

        isSkillProcess = false;
    }
}
