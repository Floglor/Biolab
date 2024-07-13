using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterController : MonoBehaviour
{
    public static WaterController Instance;
    public Sprite waterSprite;
    public List<Sprite> WaterSprites;
    private Tilemap _waterTilemap;
    public IWaterScanner WaterScanner;
    public List<CustomWaterTile> WaterTiles;


    private void Start()
    {
        WaterScanner = GetComponent<IWaterScanner>();

        Instance = this;
        _waterTilemap = GetComponent<Tilemap>();
        WaterTiles = new List<CustomWaterTile>();
        PrepareWaterTiles();
    }

    private void PrepareWaterTiles()
    {
        foreach (Vector3Int position in _waterTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int cellPosition = new Vector3Int(position.x, position.y, 0);
            if (!_waterTilemap.HasTile(cellPosition)) continue;

            Tile tile = _waterTilemap.GetTile<Tile>(cellPosition);

            bool isWaterFlag = false;
            
            foreach (Sprite sprite in WaterSprites)
            {
                if (tile.sprite == sprite) isWaterFlag = true;
            }
            
            if (!isWaterFlag) continue;
            
            //if (tile.sprite != waterSprite) continue;
            PrepareAndAddWaterTile(tile, cellPosition);
        }
    }

    private void PrepareAndAddWaterTile(Tile tile, Vector3Int cellPosition)
    {
        CustomWaterTile waterTile = new CustomWaterTile(tile, cellPosition, _waterTilemap.CellToWorld(cellPosition));
        WaterTiles.Add(waterTile);
    }
}