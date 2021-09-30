using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/MatingDecision")]
    public class MatingDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.timersHandler.GetOrCreateTimer(TimerName.Mating, GlobalValues.Instance
                       .searchMateDelay).TimerIsRunning != true &&
                   !(controller.actingCreature.reproductionNeed <= GlobalValues.Instance.matingNeedHighTreshold) &&
                   !(controller.actingCreature.reproductionNeed < controller.actingCreature.thirst) &&
                   !(controller.actingCreature.reproductionNeed < controller.actingCreature.hunger);
        }
    }
}