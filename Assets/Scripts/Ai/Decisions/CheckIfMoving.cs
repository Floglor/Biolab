using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/CheckIfMoving")]
    public class CheckIfMoving : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.actingCreature.aiPath.hasPath;
        }
    }
}