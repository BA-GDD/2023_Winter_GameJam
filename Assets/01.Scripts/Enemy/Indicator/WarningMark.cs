using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarningMark : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _timer = 0f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init() {
        Debug.Log("TestEnable");
        _timer = 0f;
        _spriteRenderer.DOFade(0f, 2f).SetLoops(4, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        Debug.Log("TestEnable");
        _timer = 0f;
        _spriteRenderer.DOFade(0f, 2f).SetLoops(4, LoopType.Yoyo);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2f)
        {
            Debug.Log("µé¾Æ°¨");
            EnemySpawner.Instance.SetIndicateMark(false);
        }
    }
}
