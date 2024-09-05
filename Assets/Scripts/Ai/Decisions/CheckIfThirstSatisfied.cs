using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/IfThirstSatisfied")]
    public class CheckIfThirstSatisfied : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.actingCreature.Thirst <= 5f;
        }
    }
}