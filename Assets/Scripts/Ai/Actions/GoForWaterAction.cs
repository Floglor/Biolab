using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/GoForWaterAction")]
    public class GoForWaterAction : Action
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.MovingBehaviour.MoveToDestination(controller.actingCreature,
                controller.actingCreature.lastWaterPosition);
        }
    }
}