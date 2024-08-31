using System.Linq;
using Ai.Actions;
using CreatureSystems;
using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/FindClosestHerbivore")]
    public class FindClosestHerbivore : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            foreach (Creature creature in CreatureList.Instance.SortByDistance(controller.actingCreature.transform
                .position).Where(creature => creature.isHerbivore))
            {
                controller.targetCreature = creature;
                break;
            }

            if (controller.targetCreature != null)
                controller.actingCreature.RepeatMoveBehaviour.StartRepeatMove(controller.actingCreature,
                    controller.targetCreature.transform);
        }
    }
}