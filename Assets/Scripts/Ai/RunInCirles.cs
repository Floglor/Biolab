using Ai.Infrastructure;
using Infrastructure;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/SeekVegetables")]
    public class RunInCirles : Action
    {
        [FormerlySerializedAs("randomWanderRadius")]
        public int RandomWanderRadius;

        public float WaitTimer = 2f;

        public override void Act(StateController controller)
        {
            Wander(controller);
        }

        private void Wander(StateController controller)
        {
            //if (controller.timersHandler.GetOrCreateTimer(TimerName.RunAway, 0.4f).TimerIsRunning) return;


            AIPath ai = controller.actingCreature.aiPath;

            if (ai.pathPending || !ai.reachedEndOfPath && ai.hasPath) return;

            if (!controller.timersHandler.GetOrCreateTimer(TimerName.Waiting, WaitTimer).TimerIsRunning &&
                controller.timersHandler.GetOrCreateTimer(TimerName.Waiting, WaitTimer).TimerReset)
            {
                controller.timersHandler.GetOrCreateTimer(TimerName.Waiting, WaitTimer).StartTimer();
                controller.timersHandler.GetOrCreateTimer(TimerName.Waiting, WaitTimer).TimerReset = false;
            }

            if (controller.timersHandler.GetOrCreateTimer(TimerName.Waiting, WaitTimer).TimerIsRunning) return;
            
            ai.destination = Utils.PickRandomPoint(RandomWanderRadius, controller.actingCreature.transform);
            ai.SearchPath();
            controller.timersHandler.GetOrCreateTimer(TimerName.Waiting, WaitTimer).TimerReset = true;
        }
    }
}