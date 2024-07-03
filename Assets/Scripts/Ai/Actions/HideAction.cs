using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/HideAction")]
    public class HideAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.Hide();
        }
    }
}