using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/GoToInitialAction")]
    public class GoToInitialAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.GoToInitialState();
        }
    }
}