using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LookForWater")]
    public class ThirstDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            if (controller.actingCreature.thirst <= GlobalValues.Instance.basicNeedLowThreshold ||
                controller.actingCreature.thirst < controller.actingCreature.hunger) return false;
            bool waterVisible = SearchForWater(controller);
            return waterVisible;
        }

        private static bool SearchForWater(StateController controller)
        {
            if (!controller.searchAvailable) return false;


            CustomWaterTile waterTile = WaterController.Instance.WaterScanner.ScanForWater(
                controller.actingCreature.transform.position,
                controller.actingCreature.eyesight, WaterController.Instance.WaterTiles);


            if (waterTile == null) return false;

            controller.actingCreature.lastWaterPosition = waterTile.WorldPosition;

            return true;
        }
    }
}