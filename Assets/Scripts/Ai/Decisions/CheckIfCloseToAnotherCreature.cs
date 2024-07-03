using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/CheckIfCloseToAnotherCreature")]
    public class CheckIfCloseToAnotherCreature : Decision
    {
        public override bool Decide(StateController controller)
        {
            Vector3 position = controller.actingCreature.transform.position;
            if (controller.targetCreature != null)
                return Vector3.Distance(position, controller.targetCreature.transform.position) <=
                       GlobalValues.Instance.closeDistance;
            
            controller.GoToInitialState();
            return false;



        }
    }
}