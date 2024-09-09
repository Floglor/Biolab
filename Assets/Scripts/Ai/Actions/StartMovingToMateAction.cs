using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/StartMovingToMateAction")]
    public class StartMovingToMateAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            if (controller.lastTargetedMate == null) return;
            
            controller.actingCreature.MovingBehaviour.MoveToDestination(controller.actingCreature,
                controller.lastTargetedMate.transform.position);
        }
    }
}