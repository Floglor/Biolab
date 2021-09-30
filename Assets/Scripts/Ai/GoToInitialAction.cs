using UnityEngine;

namespace Ai
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