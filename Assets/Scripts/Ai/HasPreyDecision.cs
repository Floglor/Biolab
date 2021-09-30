using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/HasPreyDecision")]
    public class HasPreyDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            controller.timersHandler.ResetTimer(TimerName.Hunt);
            return controller.targetCreature != null;
        }
    }
}