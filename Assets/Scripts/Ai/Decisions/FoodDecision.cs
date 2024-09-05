using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LookForFood")]
    public class FoodDecision : Decision
    {
        private CustomTile foodTile;

        public override bool Decide(StateController controller)
        {
            if (controller.actingCreature.Hunger <= GlobalValues.Instance.basicNeedLowThreshold ||
                controller.actingCreature.Hunger < controller.actingCreature.Thirst) return false;
            bool foodVisible = SearchForFood(controller);
            return foodVisible;
        }

        private bool SearchForFood(StateController controller)
        {
            if (!controller.searchAvailable) return false;

            foodTile = FoodController.Instance.ScanForFood(controller.actingCreature.transform.position);

            if (foodTile == null)
            {
                Debug.Log("Can't find food!");
                return false;
            }

            controller.actingCreature.lastFoodPosition = foodTile.WorldPosition;
            controller.actingCreature.LastFoodTile = foodTile;


            return true;
        }
    }
}