using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/IfThirstSatisfied")]
    public class CheckIfThirstSatisfied : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.actingCreature.thirst <= 5f;
        }
    }
}