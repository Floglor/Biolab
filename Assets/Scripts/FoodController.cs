using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FoodController : MonoBehaviour
{
    public static FoodController Instance;
    public float growthPerSecond;
    public float updateFoodTimer;

    public List<Corpse> corpses;
    
    public List<Sprite> FoodSprites;

    private Tilemap _foodTilemap;
    private List<CustomTile> _foodTiles;
    private TilemapRenderer _tilemapRenderer;
    private float _updateFoodRealTimer;

    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _tilemapRenderer = GetComponent<TilemapRenderer>();
        _updateFoodRealTimer = 0f;
        _foodTilemap = GetComponent<Tilemap>();
        _foodTiles = new List<CustomTile>();


        PrepareFoodTiles();
    }

    private void Update()
    {
        UpdateFood();
    }

    public CustomTile GetFoodTileWorld(Vector3 position)
    {
        foreach (CustomTile customTile in _foodTiles)
        {
            if (_foodTilemap.WorldToCell(position).Equals(customTile.CellPosition))
                return customTile;
        }

        return null;
    }
    public CustomTile GetFoodTile(Vector3 worldPosition)
    {
        foreach (CustomTile customTile in _foodTiles)
        {
            if (customTile.WorldPosition.Equals(worldPosition)) 
                return customTile;
        }

        return null;
    }
    
    public CustomTile ScanForFood(Vector3 position)
    {
        CustomTile closestCustomTile = null;
        float closestDistance = Mathf.Infinity;

        foreach (CustomTile customTile in _foodTiles)
        {
            if (customTile.FoodLevel <= 10f) continue;

            float distance = Vector3.Distance(customTile.WorldPosition, position);

            if (!(closestDistance > distance)) continue;

            closestDistance = distance;
            closestCustomTile = customTile;
        }

        return closestCustomTile;
    }

    public Corpse ScanForCorpses(Vector3 position)
    {
        Corpse closestCorpse = null;
        float closestDistance = Mathf.Infinity;

        if (corpses.IsNullOrEmpty()) return null;

        foreach (Corpse corpse in corpses)
        {
            float distance = Vector3.Distance(corpse.transform.position, position);

            if (!(closestDistance > distance)) continue;

            closestDistance = distance;
            closestCorpse = corpse;
        }

        return closestCorpse;
    }


    private void PrepareFoodTiles()
    {
        foreach (Vector3Int position in _foodTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int cellPosition = new Vector3Int(position.x, position.y, 0);

            if (!_foodTilemap.HasTile(cellPosition)) continue;

            Tile tile = _foodTilemap.GetTile<Tile>(cellPosition);
            
            CreateFoodTileAndAdd(tile, cellPosition, FoodTier.Tier100);

          //  if (tile.sprite == GlobalValues.Instance.emptyFoodSprite) continue;
//
          //  if (tile.sprite == GlobalValues.Instance.fullFoodSprite)
          //      CreateFoodTileAndAdd(tile, cellPosition, FoodTier.Tier100);
          //  else if (tile.sprite == GlobalValues.Instance.halfFoodSprite)
          //      CreateFoodTileAndAdd(tile, cellPosition, FoodTier.Tier50);
          //  else if (tile.sprite == GlobalValues.Instance.quarterFoodSprite)
          //      CreateFoodTileAndAdd(tile, cellPosition, FoodTier.Tier25);
        }
    }

    private void CreateFoodTileAndAdd(Tile tile, Vector3Int cellPosition, FoodTier foodTier)
    {
        CustomTile fullFoodTile;

        switch (foodTier)
        {
            case FoodTier.Tier100:
                fullFoodTile = new CustomTile(tile, foodTier, 100f, 100f, cellPosition,
                    _foodTilemap.CellToWorld(cellPosition));
                _foodTiles.Add(fullFoodTile);
                break;
            case FoodTier.Tier50:
                fullFoodTile = new CustomTile(tile, foodTier, 50f, 50f, cellPosition,
                    _foodTilemap.CellToWorld(cellPosition));
                _foodTiles.Add(fullFoodTile);
                break;
            case FoodTier.Tier25:
                fullFoodTile = new CustomTile(tile, foodTier, 25f, 25f, cellPosition,
                    _foodTilemap.CellToWorld(cellPosition));
                _foodTiles.Add(fullFoodTile);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(foodTier), foodTier, null);
        }

        // Instantiate(GlobalValues.Instance.debugObj, fullFoodTile.WorldPosition, Quaternion.identity);
    }

    private void UpdateFood()
    {
        _updateFoodRealTimer += Time.deltaTime;

        if (!(_updateFoodRealTimer >= updateFoodTimer)) return;

        GrowFood();
        _updateFoodRealTimer = 0f;
    }

    private void GrowFood()
    {
        foreach (CustomTile customTile in _foodTiles)
        {
            customTile.FoodLevel += growthPerSecond * updateFoodTimer;
            customTile.UpdateVisuals();
        }
    }
}