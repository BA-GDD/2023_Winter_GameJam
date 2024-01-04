using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Gun
{
    [SerializeField]
    private ParticleSystem _skillEffect;
    [SerializeField]
    private ParticleSystem _skillEffect_01;
    [SerializeField]
    private TextMeshPro _skillText;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private float _skillLifeTime;
    [Range(0f, 180f), SerializeField]
    private float _shotAngleRange = 100f;
    [Range(0f, 180f), SerializeField]
    private float _skillShotAngleRange = 150f;
    [SerializeField]
    private float _speedRange;
    [SerializeField]
    private int _shotsAmount = 7;
    [SerializeField]
    private int _skillShotsAmount = 15;
    [SerializeField]
    private int _maximumSkillShot = 5;
    private int _skillShotCount;

    [SerializeField]
    private FeedbackPlayer _feedbackPlayer;
    protected override void Update()
    {
        base.Update();

        if (owner.transform.localScale.x * _skillText.transform.localScale.x < 0f)
        {
            FlipText();
        }
    }

    public override void ShootProcess()
    {
        if (_skillShotCount <= 0)
        {
            for (int i = 0; i < _shotsAmount; ++i)
            {
                PlayerBullet bullet = PoolManager.Instance.Pop(PoolingType.PlayerBullet) as PlayerBullet;
                bullet.bulletSpeed = _bulletSpeed;
                bullet.bulletSpeed += Random.Range(-_speedRange, _speedRange);
                bullet.lifeTime = _lifeTime;
                bullet.transform.position = firePosition.position;
                Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - bullet.transform.position;

                direction.Normalize();

                float angle = Random.Range(-_shotAngleRange * 0.5f, _shotAngleRange * 0.5f);
                bullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angle, Vector3.forward);
                bullet.ApeendEvent(KillEvnetHandle);
            }
        }
        else
        {
            if (--_skillShotCount <= 0)
            {
                _skillEffect.Stop();
                _skillText.gameObject.SetActive(false);
            }
            else
            {
                _skillText.text = _skillShotCount.ToString();
            }

            for (int i = 0; i < _skillShotsAmount; ++i)
            {
                PlayerBullet bullet = PoolManager.Instance.Pop(PoolingType.PlayerBullet) as PlayerBullet;
                bullet.bulletSpeed = _bulletSpeed;
                bullet.bulletSpeed += Random.Range(-_speedRange, _speedRange);
                bullet.lifeTime = _skillLifeTime;
                bullet.transform.position = firePosition.position;
                Vector2 direction = _mainCam.ScreenToWorldPoint(Mouse.current.position.value) - bullet.transform.position;

                direction.Normalize();

                float angle = Random.Range(-_skillShotAngleRange * 0.5f, _skillShotAngleRange * 0.5f);
                bullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angle, Vector3.forward);
                bullet.ApeendEvent(KillEvnetHandle);
            }
        }
    }

    public override void Skill(bool occurSkill)
    {
        if (CanUseSkill())
        {
            _skillShotCount = _maximumSkillShot;

            _skillText.gameObject.SetActive(true);

            _skillText.text = _skillShotCount.ToString();

            _feedbackPlayer.PlayFeedback();

            _skillEffect.Play();
            _skillEffect_01.Play();
            base.Skill(occurSkill);
        }
    }

    public void FlipText()
    {
        _skillText.rectTransform.anchoredPosition *= new Vector2(-1f, 1f);
        _skillText.transform.localScale *= new Vector2(-1f, 1f);
    }
}
