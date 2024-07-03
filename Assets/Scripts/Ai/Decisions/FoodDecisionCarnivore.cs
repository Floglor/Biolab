using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LookForFoodCarnivore")]
    public class FoodDecisionCarnivore : Decision
    {
        private Corpse corpse;

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

            corpse = FoodController.Instance.ScanForCorpses(controller.actingCreature.transform.position);

            if (corpse == null) return false;

            controller.actingCreature.lastFoodPosition = corpse.transform.position;
            controller.actingCreature.lastCorpse = corpse;


            return true;
        }
    }
}