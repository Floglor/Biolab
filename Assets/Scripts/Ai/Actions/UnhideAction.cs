using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/UnhideAction")]
    public class UnhideAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.ShowYourself();
        }
    }
}