using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/StopRepeatMoveAction")]
    public class StopRepeatMoveAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.RepeatMoveBehaviour.StopRepeatMove();
        }
    }
}