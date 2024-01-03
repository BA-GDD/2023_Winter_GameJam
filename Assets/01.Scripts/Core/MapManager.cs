using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System;

public enum TileType
{
    Ground,
    Water
}
public enum SpaType
{
    None,
    SpeedUp,
    Blind,

}

[System.Serializable]
public struct SpaData
{
    public SpaType type;
    public Color tintColor;
    public int weight;
}

public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField] private List<SpaData> hotSprings;
    [SerializeField] private Tilemap _groundMap;
    [SerializeField] private Tilemap _holeMap;

    [SerializeField] private TileBase _holeTile;
    [SerializeField] private TileBase _groundTile;

    [SerializeField] private Vector2Int _defaultSpaSize;

    private Spa _spa;
    private EffectPlayer[,] _smokes;
    private Vector2Int _mapSize;
    public float WaterFillAmount()
    {
        int waterCnt = _holeMap.GetTilesBlock(_holeMap.cellBounds).Length;
        int groundCnt = _groundMap.GetTilesBlock(_groundMap.cellBounds).Length;
        return (float)waterCnt / groundCnt;
    }


    private void Start()
    {

        SetRandomSpa();

        Vector2Int minPos = Vector2Int.zero - _defaultSpaSize / 2;
        Vector2Int maxPos = Vector2Int.zero + _defaultSpaSize / 2;
        _groundMap.CompressBounds();
        BoundsInt bounds = _groundMap.cellBounds;
        _mapSize = new Vector2Int(bounds.xMax, bounds.yMax);
        _smokes = new EffectPlayer[_mapSize.x, _mapSize.y];
        for (int x = minPos.x; x <= maxPos.x; x++)
        {
            for (int y = minPos.y; y <= maxPos.y; y++)
            {
                _holeMap.SetTile(new Vector3Int(x, y), _holeTile);
                EffectPlayer fx = PoolManager.Instance.Pop(PoolingType.SpaSmoke) as EffectPlayer;
                fx.StartPlay(-1);
                fx.transform.position = new Vector2(x, y);
                _smokes[Mathf.FloorToInt(x + bounds.xMax * 0.5f), Mathf.FloorToInt(y + bounds.yMax * 0.5f)] = fx;
            }
        }
        _holeMap.CompressBounds();
    }

    private void SetRandomSpa()
    {
        int total = 0;
        foreach (var item in hotSprings)
        {
            total += item.weight;
        }
        hotSprings.OrderBy((x) => x.weight);
        float pivot = UnityEngine.Random.value;
        float value = 0;
        foreach (var item in hotSprings)
        {
            value += (float)item.weight / total;
            if (value >= pivot)
            {
                Type t = Type.GetType($"{item.type}Spa");
                _spa = Activator.CreateInstance(t) as Spa;
                _holeMap.color = item.tintColor;
                break;
            }
        }
    }

    public bool CheckWater(Vector3 pos)
    {
        pos.z = 0;
        return _holeMap.HasTile(Vector3Int.CeilToInt(pos));
    }


    public void SetTile(Vector2 pos, TileType type)
    {
        Vector3Int intVec = new Vector3Int(Mathf.CeilToInt(pos.x), Mathf.CeilToInt(pos.y));
        DrawTile(intVec, type);
        _holeMap.CompressBounds();

    }
    private void DrawTile(Vector3Int pos, TileType type)
    {
        Vector3Int minPos = pos - Vector3Int.one;
        Vector3Int maxPos = pos + Vector3Int.one;
        switch (type)
        {
            case TileType.Ground:
                for (int x = minPos.x; x <= maxPos.x; x++)
                {
                    for (int y = minPos.y; y <= maxPos.y; y++)
                    {
                        _holeMap.SetTile(new Vector3Int(x, y), null);
                        PoolManager.Instance.Push(_smokes[x, y]);
                        _smokes[Mathf.FloorToInt(x + _mapSize.x * 0.5f), Mathf.FloorToInt(y + _mapSize.y * 0.5f)] = null;
                    }
                }
                break;
            case TileType.Water:
                for (int x = minPos.x; x <= maxPos.x; x++)
                {
                    for (int y = minPos.y; y <= maxPos.y; y++)
                    {
                        Vector3Int intPos = new Vector3Int(x, y);
                        if (!_holeMap.HasTile(intPos))
                        {
                            EffectPlayer fx = PoolManager.Instance.Pop(PoolingType.SpaSmoke) as EffectPlayer;
                            fx.transform.position = intPos;
                            _smokes[Mathf.FloorToInt(x + _mapSize.x * 0.5f), Mathf.FloorToInt(y + _mapSize.y * 0.5f)] = fx;
                        }
                        _holeMap.SetTile(intPos, _holeTile);

                    }
                }
                //_holeMap.SetTile(new Vector3Int(pos.x, pos.y), _holeTile);
                //_groundMap.SetTile(new Vector3Int(pos.x, pos.y), null);
                break;
        }
    }
}
