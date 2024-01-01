using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PerlinNoiseMapGenerator
{
    private MapGenerator _generator;
    private RectInt _roomRect;

    public PerlinNoiseMapGenerator(MapGenerator mapGenerator, RectInt roomRect)
    {
        _generator = mapGenerator;
        _roomRect = roomRect;
    }

    public async Task Generate()
    {
        await CreateFloors();
    }

    private async Task CreateFloors()
    {
        float magnification = _generator.magnification;
        int tileTypeCnt = Enum.GetValues(typeof(MapType)).Length - 1;
        for (int x = _roomRect.x; x < _roomRect.x + _roomRect.width; x++)
        {
            for (int y = _roomRect.y; y < _roomRect.y + _roomRect.height; y++)
            {
                float raw_perlin = Mathf.PerlinNoise(x / magnification, y / magnification);
                float clamp_perlin = Mathf.Clamp01(raw_perlin); // Thanks: youtu.be/qNZ-0-7WuS8&lc=UgyoLWkYZxyp1nNc4f94AaABAg
                float scaled_perlin = clamp_perlin * tileTypeCnt;

                MapType type = (MapType)Mathf.FloorToInt(scaled_perlin);
                //Debug.Log(Mathf.CeilToInt(scaled_perlin));
                _generator.gridHandler[x, y] = type;

                await Task.Delay(Mathf.RoundToInt(_generator.waitWalkerTime * 1000));
            }
        }
    }
}
