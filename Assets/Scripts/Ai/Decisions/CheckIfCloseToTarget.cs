using Ai.Infrastructure;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/CheckIfCloseToFood")]
    public class CheckIfCloseToTarget : Decision
    {
        public override bool Decide(StateController controller)
        {
            Vector3 position = controller.actingCreature.transform.position;
            Vector3 targetTile = controller.actingCreature.aiPath.destination;

            return Vector3.Distance(position, targetTile) <= Creature.EatingRange;
        }
    }
}