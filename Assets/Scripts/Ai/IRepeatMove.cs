using CreatureSystems;
using UnityEngine;

namespace Ai
{
    public interface IRepeatMove
    {
        void StopRepeatMove();
        void StartRepeatMove(Creature creature, Transform transformToFollow);
    }
}