using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/MatingDecision")]
    public class MatingDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            if (controller.actingCreature.WeightSystem.ReturnNourishment() < GlobalValues.Instance.lowNourishmentThreshold)
            {
                return false;
            }
            
            
            return controller.timersHandler.GetOrCreateTimer(TimerName.Mating, GlobalValues.Instance
                       .searchMateDelay).TimerIsRunning != true &&
                   !(controller.actingCreature.ReproductionNeed <= GlobalValues.Instance.matingNeedHighThreshold) &&
                   !(controller.actingCreature.ReproductionNeed < controller.actingCreature.Thirst) &&
                   !(controller.actingCreature.ReproductionNeed < controller.actingCreature.Hunger) &&
                   !(controller.actingCreature.ReproductionNeed > controller.actingCreature.WeightSystem.ReturnNourishment()/1.5f);
        }
    }
}