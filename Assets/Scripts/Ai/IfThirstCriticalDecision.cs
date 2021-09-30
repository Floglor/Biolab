using UnityEngine;

namespace Ai
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