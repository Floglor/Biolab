using CreatureSystems;
using UnityEngine;

namespace Ai
{
    public interface IMovingBehaviour
    {
        void MoveToDestination(Creature creature, Vector3 target);
    }
}