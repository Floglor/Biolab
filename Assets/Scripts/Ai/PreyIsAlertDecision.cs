using UnityEngine;

namespace Ai
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