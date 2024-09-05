using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/IfHungerSatisfied")]
    public class IfHungerSatisfied : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.actingCreature.Hunger <= 5f;
        }
    }
}