using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/CheckIfPartnerIsStillInterested")]
    public class CheckIfPartnerIsStillInterested : Decision
    {
        
        public override bool Decide(StateController controller)
        {
            if (controller.timersHandler.GetOrCreateTimer(TimerName.PartnerInterested, 0.4f).TimerIsRunning)
            {
                return true;
            }

            return controller.lastTargetedMate.stateController.currentState != controller.runFromDangerState;
        }
    }
}