using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/HasMateDecision")]
    public class HasMateDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.lastTargetedMate != null;
        }
    }
}