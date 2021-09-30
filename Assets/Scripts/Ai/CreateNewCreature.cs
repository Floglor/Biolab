using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/CreateNewCreature")]
    public class CreateNewCreature : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            if (controller.actingCreature.isMale) return;

            CreatureSpawner.Instance.SpawnNewCreature(controller.actingCreature, controller.lastTargetedMate,
                (Vector3) Random.insideUnitCircle + controller.actingCreature.transform.position);

            controller.actingCreature.reproductionNeed = 0f;
            controller.lastTargetedMate.reproductionNeed = 0f;
            controller.lastTargetedMate = null;
            controller.targetCreature = null;
        }
    }
}