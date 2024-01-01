using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WalkerGenerator
{
    private MapGenerator _generator;

    private int _maximumWalkers = 5;

    private int _tileCount = default;
    private List<WalkerObject> _walkers;
    private RectInt _roomRect;

    public WalkerGenerator(MapGenerator generator, RectInt roomRect, int maxWalker = 5)
    {
        _generator = generator;
        _roomRect = roomRect;
        _maximumWalkers = maxWalker;
    }

    public async Task Generate()
    {
        Init();
        await CreateFloors();
    }

    private void Init()
    {
        _walkers = new List<WalkerObject>();

        Vector3Int TileCenter = new Vector3Int(_roomRect.x + _roomRect.width / 2, _roomRect.y + _roomRect.height / 2);

        WalkerObject curWalker = new WalkerObject(TileCenter, GetDirection(), 0.5f);
        _generator.gridHandler[TileCenter.x, TileCenter.y] = MapType.FLOOR;
        _walkers.Add(curWalker);

        _tileCount++;
    }

    private Vector3Int GetDirection()
    {
        int choice = UnityEngine.Random.Range(0, 5);
        switch (choice)
        {
            case 0:
                return Vector3Int.down;
            case 1:
                return Vector3Int.left;
            case 2:
                return Vector3Int.up;
            case 3:
                return Vector3Int.right;
            default:
                return Vector3Int.zero;
        }
    }
    private async Task CreateFloors()
    {
        while ((float)_tileCount / (float)(_roomRect.width * _roomRect.height) < _generator.fillPercentage)
        {
            Debug.Log(_walkers.Count);
            bool hasCreatedFloor = false;
            foreach (var curWalker in _walkers)
            {
                Vector3Int curPos = new Vector3Int((int)curWalker.position.x, (int)curWalker.position.y);

                if (_generator.gridHandler[curPos.x, curPos.y] != MapType.FLOOR)
                {
                    _tileCount++;
                    _generator.gridHandler[curPos.x, curPos.y] = MapType.FLOOR;
                    hasCreatedFloor = true;
                }
            }

            ChangeToRemove();
            ChangeToRedirect();
            ChangeToCreate();
            UpdatePosition();

            if (hasCreatedFloor)
                await Task.Delay(Mathf.RoundToInt(_generator.waitWalkerTime * 1000));
        }
    }

    private void ChangeToRemove()
    {
        for (int i = 0; i < _walkers.Count; ++i)
        {
            if (UnityEngine.Random.value < _walkers[i].chanceToChange && _walkers.Count > 1)
            {
                _walkers.RemoveAt(i);
                break;
            }
        }
    }
    private void ChangeToRedirect()
    {
        for (int i = 0; i < _walkers.Count; ++i)
        {
            if (UnityEngine.Random.value < _walkers[i].chanceToChange)
            {
                _walkers[i].direction = GetDirection();
            }
        }
    }

    private void ChangeToCreate()
    {
        for (int i = 0; i < _walkers.Count; ++i)
        {
            if (UnityEngine.Random.value < _walkers[i].chanceToChange && _walkers.Count < _maximumWalkers)
            {
                Vector3Int newDirection = GetDirection();
                Vector3Int newPosition = _walkers[i].position;

                WalkerObject newWalker = new WalkerObject(newPosition, newDirection, 0.5f);
                _walkers.Add(newWalker);
            }
        }
    }

    private void UpdatePosition()
    {
        for (int i = 0; i < _walkers.Count; ++i)
        {
            WalkerObject walker = _walkers[i];
            walker.position += walker.direction;
            walker.position.x = Mathf.Clamp(walker.position.x, _roomRect.x, _roomRect.x + _roomRect.width);
            walker.position.y = Mathf.Clamp(walker.position.y, _roomRect.y, _roomRect.y + _roomRect.height);
            _walkers[i] = walker;
        }
    }
}
