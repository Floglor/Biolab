using UnityEngine;

namespace Ai
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