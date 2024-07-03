using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/IfThirstCriticalDecision")]
    public class IfThirstCriticalDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.actingCreature.thirst >= GlobalValues.Instance.basicNeedHighThreshold;
        }
    }
}