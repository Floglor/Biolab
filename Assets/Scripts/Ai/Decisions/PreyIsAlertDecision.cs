using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/PreyIsAlertDecision")]
    public class PreyIsAlertDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.targetCreature.isAlert;
        }
    }
}