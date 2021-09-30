using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/IfHungerSatisfied")]
    public class IfHungerSatisfied : Decision
    {
        public override bool Decide(StateController controller)
        {
            return controller.actingCreature.hunger <= 5f;
        }
    }
}