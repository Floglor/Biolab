using UnityEngine;

public interface IMovingBehaviour
{
    void MoveToDestination(Creature creature, Vector3 target);
}