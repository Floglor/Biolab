using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/SeekMateAction")]
    public class SeekMateAction : Action
    {
        public override void Act(StateController controller)
        {
            controller.lastTargetedMate = null;
                
            if (controller.isWaitingForMateAnswer) return;
          ///  if (controller.lastTargetedMate != null) return;

            IMateSeeker mateSeeker = controller.actingCreature.GetComponent<IMateSeeker>();
            controller.lastTargetedMate = mateSeeker.SeekMate(controller.actingCreature);

            if (controller.lastTargetedMate == null) return;


            controller.lastTargetedMate.GetComponent<IMateSeeker>()
                .BeTargeted(controller.lastTargetedMate, controller.actingCreature);
            controller.targetCreature = controller.lastTargetedMate;
        }
    }
}