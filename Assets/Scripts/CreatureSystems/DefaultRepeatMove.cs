using System.Collections;
using Ai;
using UnityEngine;

namespace CreatureSystems
{
    public class DefaultRepeatMove : MonoBehaviour, IRepeatMove
    {
        private Coroutine _repeatMove;

        public void StartRepeatMove(Creature creature, Transform transformToFollow)
        {
            if (_repeatMove != null)
                StopCoroutine(_repeatMove);
            _repeatMove = StartCoroutine(RepeatMoveToObject(creature, transformToFollow));
        }


        public void StopRepeatMove()
        {
            if (_repeatMove != null)
                StopCoroutine(_repeatMove);
        }

        private IEnumerator RepeatMoveToObject(Creature creature, Transform transform)
        {
            while (true)
            {
                if (transform == null) break;
                creature.MovingBehaviour.MoveToDestination(creature, transform.position);
                yield return new WaitForSeconds(GlobalValues.Instance.repeatMoveDelay);
            }
        }
    }
}