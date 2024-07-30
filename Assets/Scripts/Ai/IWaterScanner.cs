using System.Collections.Generic;
using UnityEngine;

public interface IWaterScanner
{
    CustomWaterTile ScanForWater(Vector3 position, float eyeSight, List<CustomWaterTile> waterTiles);
}