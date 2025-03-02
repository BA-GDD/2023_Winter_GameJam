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

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get => _instance;
        set => _instance = value;
    }  

    [SerializeField] private List<SpaData> hotSprings;
    [SerializeField] private Tilemap _groundMap;
    [SerializeField] private Tilemap _holeMap;
    [SerializeField] private Tilemap _decoMap;

    [SerializeField] private TileBase _holeTile;
    [SerializeField] private TileBase _groundTile;

    [SerializeField] private Vector2Int _defaultSpaSize;

    private Spa _spa;
    private EffectPlayer[,] _smokes;
    private List<Vector2Int> _waterPos = new List<Vector2Int>();
    private Vector2Int _mapSize;
    public float WaterFillAmount()
    {
        int waterCnt = 0;
        foreach (var item in _holeMap.GetTilesBlock(_holeMap.cellBounds))
        {
            if (item != null)
            {
                waterCnt++;
            }
        }
        int groundCnt = _groundMap.GetTilesBlock(_groundMap.cellBounds).Length;
        return (float)waterCnt / groundCnt;
    }
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {

        SetRandomSpa();

        Vector2Int minPos = Vector2Int.zero - _defaultSpaSize / 2;
        Vector2Int maxPos = Vector2Int.zero + _defaultSpaSize / 2;
        _groundMap.CompressBounds();
        BoundsInt bounds = _groundMap.cellBounds;
        _mapSize = new Vector2Int(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin);
        _smokes = new EffectPlayer[_mapSize.x + 1, _mapSize.y + 1];
        for (int x = minPos.x; x <= maxPos.x; x++)
        {
            for (int y = minPos.y; y <= maxPos.y; y++)
            {
                Vector3Int intPos = new Vector3Int(x, y);
                if (_decoMap.HasTile(intPos)) continue;

                _holeMap.SetTile(intPos, _holeTile);
                EffectPlayer fx = PoolManager.Instance.Pop(PoolingType.SpaSmoke) as EffectPlayer;
                fx.StartPlay(-1);
                fx.transform.position = new Vector2(x + 1, y + 1);
                fx.transform.position -= new Vector3(0.5f, 0.5f);
                _waterPos.Add((Vector2Int)intPos);
                _smokes[Mathf.FloorToInt(x + _mapSize.x * 0.5f), Mathf.FloorToInt(y + _mapSize.y * 0.5f)] = fx;
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

    public bool CheckWater(Vector3 pos, out Vector3Int tile)
    {
        pos.z = 0;
        //pos.x -= 1;
        //pos.y -= 1;
        tile = Vector3Int.CeilToInt(pos);
        return _holeMap.HasTile(Vector3Int.CeilToInt(pos));
    }


    public void SetTile(Vector2 pos, TileType type)
    {
        Vector3Int intVec = new Vector3Int(Mathf.CeilToInt(pos.x - 1), Mathf.CeilToInt(pos.y - 1));
        DrawTile(intVec, type);
        _holeMap.CompressBounds();
    }

    private void DrawTile(Vector3Int pos, TileType type)
    {
        Vector3Int minPos = pos - Vector3Int.one;
        Vector3Int maxPos = pos + Vector3Int.one;
        BoundsInt bounds = _groundMap.cellBounds;
        switch (type)
        {
            case TileType.Ground:
                pos.x = Mathf.Clamp(pos.x+1, bounds.xMin, bounds.xMax);
                pos.y = Mathf.Clamp(pos.y+1, bounds.yMin, bounds.yMax);
                if (_decoMap.HasTile(pos)) return;

                if (_holeMap.HasTile(pos))
                {
                    PoolManager.Instance.Push(_smokes[pos.x + Mathf.FloorToInt(_mapSize.x * 0.5f), pos.y + Mathf.FloorToInt(_mapSize.y * 0.5f)]);
                    _smokes[Mathf.FloorToInt(pos.x + _mapSize.x * 0.5f), Mathf.FloorToInt(pos.y + _mapSize.y * 0.5f)] = null;
                    _waterPos.Remove((Vector2Int)pos);
                }
                _holeMap.SetTile(pos, null);
                if (GameManager.Instance.occupationPercent < WaterFillAmount())
                    GameManager.Instance.GameEnd();

                break;
            case TileType.Water:
                for (int x = minPos.x; x <= maxPos.x; x++)
                {
                    x = Mathf.Clamp(x, bounds.xMin, bounds.xMax);
                    for (int y = minPos.y; y <= maxPos.y; y++)
                    {
                        y = Mathf.Clamp(y, bounds.yMin, bounds.yMax);
                        Vector3Int intPos = new Vector3Int(x, y);

                        if (_decoMap.HasTile(intPos)) continue;

                        if (!_holeMap.HasTile(intPos))
                        {
                            EffectPlayer fx = PoolManager.Instance.Pop(PoolingType.SpaSmoke) as EffectPlayer;
                            fx.transform.position = intPos + (Vector3Int)Vector2Int.one;
                            fx.transform.position -= new Vector3(0.5f, 0.5f);
                            _waterPos.Add((Vector2Int)intPos);
                            _smokes[x + Mathf.FloorToInt(_mapSize.x * 0.5f), y + Mathf.FloorToInt(_mapSize.y * 0.5f)] = fx;
                        }
                        _holeMap.SetTile(intPos, _holeTile);

                    }
                }
                //_holeMap.SetTile(new Vector3Int(pos.x, pos.y), _holeTile);
                //_groundMap.SetTile(new Vector3Int(pos.x, pos.y), null);
                break;
        }
    }


    public Vector3 GetNearWater(Vector3 pos)
    {
        float nearDis = float.MaxValue;
        Vector3Int returnValue = default;
        foreach (Vector3Int p in _waterPos)
        {
            float dis = Vector3.Distance(p, pos);
            if (dis < nearDis)
            {
                returnValue = p;
                nearDis = dis;
            }
        }
        return returnValue;
    }
    public void EnterSpa()
    {
        _spa.Enter();
    }
    public void ExitSpa()
    {
        _spa.Exit();
    }
}
