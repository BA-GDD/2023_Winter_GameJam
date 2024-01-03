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
    private static MapManager _instnace;
    public static MapManager instance
    {
        get
        {
            if (_instnace == null)
            {
                _instnace = FindObjectOfType<MapManager>();
            }
            if (_instnace)
            {
                Debug.LogError($"존재하지않다[{typeof(MapManager)}].");
            }
            return _instnace;
        }
    }

    [SerializeField] private List<SpaData> hotSprings;
    [SerializeField] private Tilemap _groundMap;
    [SerializeField] private Tilemap _holeMap;

    [SerializeField] private TileBase _holeTile;
    [SerializeField] private TileBase _groundTile;

    [SerializeField] private Vector2Int _defaultSpaSize;

    private Spa spa;

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
        _holeMap.BoxFill(Vector3Int.zero, _holeTile, minPos.x, minPos.y, maxPos.x, maxPos.y);

        _holeMap.CompressBounds();
        _groundMap.CompressBounds();
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
                print(value);
            if (value >= pivot)
            {
                Type t = Type.GetType($"{item.type}Spa");
                spa = Activator.CreateInstance(t) as Spa;
                _holeMap.color = item.tintColor;
                break;
            }
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTile(Camera.main.ScreenToWorldPoint(Input.mousePosition), TileType.Water);
        }
        print(WaterFillAmount());
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
                    }
                }
                break;
            case TileType.Water:
                for (int x = minPos.x; x < maxPos.x; x++)
                {
                    for (int y = minPos.y; y < maxPos.y; y++)
                    {
                        _holeMap.SetTile(new Vector3Int(x, y), _holeTile);
                    }
                }
                //_holeMap.SetTile(new Vector3Int(pos.x, pos.y), _holeTile);
                //_groundMap.SetTile(new Vector3Int(pos.x, pos.y), null);
                break;
        }
    }
}
