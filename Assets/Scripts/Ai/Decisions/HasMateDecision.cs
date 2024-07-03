using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
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