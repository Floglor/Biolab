using System.Collections.Generic;
using UnityEngine;

namespace Ai
{
    public interface IWaterScanner
    {
        CustomWaterTile ScanForWater(Vector3 position, float eyeSight, List<CustomWaterTile> waterTiles);
    }
}