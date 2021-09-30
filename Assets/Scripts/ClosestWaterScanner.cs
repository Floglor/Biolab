using System.Collections.Generic;
using UnityEngine;

public class ClosestWaterScanner : MonoBehaviour, IWaterScanner
{
    public CustomWaterTile ScanForWater(Vector3 position, float eyeSight, List<CustomWaterTile> waterTiles)
    {
        CustomWaterTile closestWaterTile = null;

        float closestDistance = Mathf.Infinity;
        foreach (CustomWaterTile customWaterTile in waterTiles)
        {
            float distance = Vector3.Distance(customWaterTile.WorldPosition, position);

            if (!(distance < closestDistance)) continue;

            closestDistance = distance;
            closestWaterTile = customWaterTile;
        }

        return closestWaterTile;
    }
}