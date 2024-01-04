using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkThrower : MonoBehaviour
{
    private float spawnY = 655f;
    [SerializeField] private float _maxXvalue;
    [SerializeField] private float _minXValue;

    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    [SerializeField] private UnderMilk _underMilk;
    [SerializeField] private MilkStamp _milkStampPrefab;
    [SerializeField] private Vector2 _stampPos;

    [SerializeField] private GameResultSceneUI _gameResultSceneUI;

    public void ThrwoMilk(int milkCount)
    {
        Debug.Log("Milk: " + milkCount);
        GameManager.Instance.GameData.milkCoount += milkCount;
        StartCoroutine(ThrowMilkCo(milkCount));
    }

    private IEnumerator ThrowMilkCo(int milkCount)
    {
        for (int i = 0; i < milkCount; i++)
        {
            FallingMilk fm = PoolManager.Instance.Pop(PoolingType.FallingMilk) as FallingMilk;
            fm.transform.SetParent(transform);
            fm.transform.localScale = Vector3.one * 1.3f;
            fm.transform.localPosition = new Vector2(Random.Range(_minXValue, _maxXvalue), spawnY);
            fm.transform.localEulerAngles = new Vector3(0, 0, Random.Range(_minAngle, _maxAngle));
            fm.Fall(_underMilk);

            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }

        yield return new WaitForSeconds(2f);

        MilkStamp ms = Instantiate(_milkStampPrefab, transform);
        ms.transform.localPosition = _stampPos;
        ms.count = milkCount;

        _gameResultSceneUI.isEndTimeLine = true;
    }
}
