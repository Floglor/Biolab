using Ai.Infrastructure;
using ModestTree;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/HuntDecision")]
    public class HuntDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return !controller.timersHandler.GetOrCreateTimer(TimerName.Hunt, GlobalValues.Instance.huntCooldown)
                       .TimerIsRunning &&
                   !(controller.actingCreature.Hunger <= GlobalValues.Instance.minHuntDecisionHungerThreshold) &&
                   !(controller.actingCreature.Hunger < controller.actingCreature.Thirst) &&
                   (FoodController.Instance.corpses.IsEmpty());
        }
    }
}