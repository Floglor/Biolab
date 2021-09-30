using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/StartRunning")]
    public class StartRunning : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.StartRunning();
        }
    }
}