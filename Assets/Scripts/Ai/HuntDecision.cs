using ModestTree;
using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/HuntDecision")]
    public class HuntDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return !controller.timersHandler.GetOrCreateTimer(TimerName.Hunt, GlobalValues.Instance.huntCooldown)
                       .TimerIsRunning &&
                   !(controller.actingCreature.hunger <= GlobalValues.Instance.minHuntDecisionHungerThreshold) &&
                   !(controller.actingCreature.hunger < controller.actingCreature.thirst) &&
                   (FoodController.Instance.corpses.IsEmpty());
        }
    }
}