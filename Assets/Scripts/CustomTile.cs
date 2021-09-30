using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum FoodTier
{
    Tier100,
    Tier50,
    Tier25
}

public class CustomTile
{
    private readonly Tile _linkedTile;
    private Vector3Int _cellPosition;
    private FoodTier _foodTier;
    private float _maxFoodLevel;
    public float FoodLevel;
    public Vector3 WorldPosition;

    public CustomTile(Tile linkedTile, FoodTier foodTier, float foodLevel, float maxFoodLevel, Vector3Int cellPosition,
        Vector3 worldPosition)
    {
        _linkedTile = linkedTile;
        _foodTier = foodTier;
        FoodLevel = foodLevel;
        _maxFoodLevel = maxFoodLevel;
        _cellPosition = cellPosition;
        WorldPosition = worldPosition;
        //Debug.Log($"food world position: [{worldPosition.x}], [{worldPosition.y}], food tier: {foodTier}");
    }


    public bool GetEaten(float amount, Vector3 chomperLocation)
    {
        if (!(FoodLevel - amount >= 0.5f) ||
            Vector3.Distance(chomperLocation, WorldPosition) >= Creature.EatingRange) return false;

        FoodLevel -= amount;
        return true;
    }

    public void UpdateVisuals()
    {
        if (FoodLevel > 75f)
            ChangeVisuals(FoodTier.Tier100);
        else if (FoodLevel < 75f && FoodLevel > 25f)
            ChangeVisuals(FoodTier.Tier50);
        else if (FoodLevel < 25f) ChangeVisuals(FoodTier.Tier25);
    }

    public void ChangeVisuals(FoodTier foodTierArgument)
    {
        switch (foodTierArgument)
        {
            case FoodTier.Tier100:
                _linkedTile.sprite = GlobalValues.Instance.fullFoodSprite;
                break;
            case FoodTier.Tier50:
                _linkedTile.sprite = GlobalValues.Instance.halfFoodSprite;
                break;
            case FoodTier.Tier25:
                _linkedTile.sprite = GlobalValues.Instance.quarterFoodSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(foodTierArgument), foodTierArgument, null);
        }
    }
}