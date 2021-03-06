using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LookForFood")]
    public class FoodDecision : Decision
    {
        private CustomTile foodTile;

        public override bool Decide(StateController controller)
        {
            if (controller.actingCreature.hunger <= GlobalValues.Instance.basicNeedLowThreshold ||
                controller.actingCreature.hunger < controller.actingCreature.thirst) return false;
            bool foodVisible = SearchForFood(controller);
            return foodVisible;
        }

        private bool SearchForFood(StateController controller)
        {
            if (!controller.searchAvailable) return false;

            foodTile = FoodController.Instance.ScanForFood(controller.actingCreature.transform.position);

            if (foodTile == null) return false;

            controller.actingCreature.lastFoodPosition = foodTile.WorldPosition;
            controller.actingCreature.LastFoodTile = foodTile;


            return true;
        }
    }
}