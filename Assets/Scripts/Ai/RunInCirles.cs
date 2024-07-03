using Ai.Infrastructure;
using Infrastructure;
using Pathfinding;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/SeekVegetables")]
    public class RunInCirles : Action
    {
        public int randomWanderRadius;

        public override void Act(StateController controller)
        {
            Wander(controller);
        }

        private void Wander(StateController controller)
        {
            AIPath ai = controller.actingCreature.aiPath;

            if (ai.pathPending || !ai.reachedEndOfPath && ai.hasPath) return;


            ai.destination = Utils.PickRandomPoint(randomWanderRadius, controller.actingCreature.transform);
            ai.SearchPath();
        }
    }
}