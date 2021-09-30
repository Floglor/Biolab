using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/RunInOppositeDirectionOfDangerAction")]
    public class RunInOppositeDirectionOfDangerAction : Action
    {
        public override void Act(StateController controller)
        {
            if (controller.timersHandler.GetOrCreateTimer(TimerName.RunAway, 0.4f).TimerIsRunning) return;

            Vector2 actingCreaturePosition = controller.actingCreature.transform.position;
            Vector2 targetCreaturePosition = controller.targetCreature.transform.position;

            // Debug.Log($"{controller.actingCreature.name} is going to {(actingCreaturePosition - targetCreaturePosition).normalized * 10f}. " +
            //           $"{actingCreaturePosition} - {targetCreaturePosition} is {actingCreaturePosition - targetCreaturePosition}." +
            //           $" Normalized is {(actingCreaturePosition - targetCreaturePosition).normalized}. " +
            //           $"* 10f is {(actingCreaturePosition - targetCreaturePosition).normalized * 10f}");

            controller.targetCreature.MovingBehaviour.MoveToDestination(controller.actingCreature,
                actingCreaturePosition +
                (actingCreaturePosition - targetCreaturePosition).normalized * 10f);

            controller.timersHandler.ResetTimer(TimerName.RunAway);
        }
    }
}