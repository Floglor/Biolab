using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/KillAction")]
    public class KillAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.targetCreature.Die();
        }
    }
}