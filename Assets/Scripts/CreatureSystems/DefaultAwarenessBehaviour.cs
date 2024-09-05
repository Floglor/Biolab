using System.Collections;
using Ai;
using UnityEngine;

namespace CreatureSystems
{
    public class DefaultAwarenessBehaviour : MonoBehaviour, IPredatorAwareness
    {
        public IEnumerator BeAware(Creature creature)
        {
            while (true)
            {
                if (creature.Thirst >= GlobalValues.Instance.basicNeedHighThreshold)
                    yield return new WaitForSeconds(GlobalValues.Instance.predatorAwarenessDelay);
                //if (TimerCheck(creature)) yield return new WaitForSeconds(GlobalValues.instance.predatorAwarenessDelay);
                Vector3 position = creature.transform.position;
                Collider2D[] overlappingColliders =
                    Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), creature.eyesight);

                bool predatorFound = false;

                foreach (Collider2D overlappingCollider in overlappingColliders)
                {
                    if (!overlappingCollider.tag.Equals("Creature")) continue;

                    Creature foundCreature = overlappingCollider.gameObject.GetComponent<Creature>();
                    if (!foundCreature.isHidden || foundCreature.isHerbivore) continue;

                    predatorFound = true;
                    creature.stateController.targetCreature = foundCreature;

                    if (!creature.isAlert)
                        creature.stateController.GetAlert();


                    creature.stateController.timersHandler.ResetTimer(TimerName.CalmDownTimer);
                }

                if (predatorFound == false &&
                    creature.isAlert &&
                    !creature.stateController.timersHandler
                        .GetOrCreateTimer(TimerName.CalmDownTimer, GlobalValues.Instance.calmDownFromChaseDelay)
                        .TimerIsRunning)
                    creature.stateController.CalmDown();

                yield return new WaitForSeconds(GlobalValues.Instance.predatorAwarenessDelay);
            }
        }

        private bool TimerCheck(Creature creature)
        {
            return creature.stateController.timersHandler
                .GetOrCreateTimer(TimerName.Awareness, GlobalValues.Instance.predatorAwarenessDelay / Time.timeScale)
                .TimerIsRunning;
        }
    }
}