using Ai.Actions;
using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Eat")]
    public class Eat : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.EatingBehaviour.StartEating(controller.actingCreature);
        }
    }
}