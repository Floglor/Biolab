using Ai;
using CreatureSystems;
using UnityEngine;

public class RvoMovingBehaviour : MonoBehaviour, IMovingBehaviour
{
    public void MoveToDestination(Creature creature, Vector3 target)
    {
        creature.rvoController.SetTarget(target, creature.Speed, creature.Speed);
        Vector3 delta = creature.rvoController.CalculateMovementDelta(transform.position, Time.deltaTime);
        Transform creaturePosition = creature.transform;
        creaturePosition.position += delta;
    }
}