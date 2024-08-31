using System.Collections;
using Ai;
using UnityEngine;

namespace CreatureSystems
{
    public class CarnivoresEatingBehaviour : MonoBehaviour, IEatingBehaviour
    {
        private Coroutine _chompCoroutine;
        private Coroutine _gulpCoroutine;

        public void StartDrinking(Creature creature)
        {
            if (!creature.alreadyDrinking) _gulpCoroutine = StartCoroutine(Gulp(creature));
        }

        public void StartEating(Creature creature)
        {
            if (creature.alreadyEating) return;
            _chompCoroutine = StartCoroutine(Chomp(creature));
        }

        public void CancelEating(Creature creature)
        {
            if (_chompCoroutine != null)
                StopCoroutine(_chompCoroutine);
            creature.alreadyEating = false;
        }

        public void CancelDrinking(Creature creature)
        {
            if (_gulpCoroutine != null)
                StopCoroutine(_gulpCoroutine);
        
            creature.OnDrinkingFinished.Invoke();
            creature.alreadyDrinking = false;
        }

        private IEnumerator Gulp(Creature creature)
        {
            while (true)
            {
                creature.alreadyDrinking = true;


                if (creature.thirst >= 2f)
                {
                    creature.HungerSystem.SatisfyThirst(Creature.ChompSize);
                    yield return new WaitForSeconds(1/creature.eatingSpeed / Time.timeScale);
                }
                else
                {
                    yield return new WaitForSeconds(1/creature.eatingSpeed / Time.timeScale);
                    CancelDrinking(creature);
                    break;
                }
            }
        }

        private IEnumerator Chomp(Creature creature)
        {
            while (true)
            {
                creature.alreadyEating = true;

                if (creature.lastCorpse != null &&
                    creature.lastCorpse.GetEaten(Creature.ChompSize, creature.transform.position) &&
                    creature.hunger >= 2f)
                {
                    creature.HungerSystem.SatisfyHunger(Creature.ChompSize);

                    yield return new WaitForSeconds(1/creature.eatingSpeed / Time.timeScale);
                }
                else
                {
                    yield return new WaitForSeconds(1/creature.eatingSpeed / Time.timeScale);
                    CancelEating(creature);
                    ReturnToFoodSearch(creature);
                    break;
                }
            }
        }

        private void ReturnToFoodSearch(Creature creature)
        {
            CancelEating(creature);
            creature.stateController.GoToInitialState();
        }
    }
}