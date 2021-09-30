using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomWaterTile
{
    private Vector3Int _cellPosition;
    private Tile _linkedTile;
    public Vector3 WorldPosition;

    public CustomWaterTile(Tile linkedTile, Vector3Int cellPosition, Vector3 worldPosition)
    {
        _linkedTile = linkedTile;
        _cellPosition = cellPosition;
        WorldPosition = worldPosition;
    }
}