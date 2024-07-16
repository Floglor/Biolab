using System.Collections.Generic;
using UnityEngine;

public class ClosestWaterScanner : MonoBehaviour, IWaterScanner
{
    public CustomWaterTile ScanForWater(Vector3 position, float eyeSight, List<CustomWaterTile> waterTiles)
    {
        List<CustomWaterTile> fullTiles = new List<CustomWaterTile>();
        CustomWaterTile closestWaterTile = null;

        float closestDistance = Mathf.Infinity;
        foreach (CustomWaterTile customWaterTile in waterTiles)
        {
            float distance = Vector3.Distance(customWaterTile.WorldPosition, position);

            if (!(distance < closestDistance)) continue;
            if (customWaterTile.IsFull) continue;

            closestDistance = distance;
            closestWaterTile = customWaterTile;
        }

        return closestWaterTile;
    }
}