using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/StartMovingToMateAction")]
    public class StartMovingToMateAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.MovingBehaviour.MoveToDestination(controller.actingCreature,
                controller.lastTargetedMate.transform.position);
        }
    }
}