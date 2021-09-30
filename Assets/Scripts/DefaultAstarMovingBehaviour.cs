using UnityEngine;

public class DefaultAstarMovingBehaviour : MonoBehaviour, IMovingBehaviour
{
    public void MoveToDestination(Creature creature, Vector3 target)
    {
        //Debug.Log($"Path update: move to position {target.x}, {target.y}");
        creature.aiPath.destination.Set(target.x, target.y, 0);

        Vector3 currentPosition = creature.transform.position;
        Vector3 position = new Vector3(currentPosition.x, currentPosition.y, 0);

        Vector3 destination = new Vector3(target.x, target.y, 0);

        creature.aiPath.destination = destination;
        creature.seeker.StartPath(position, destination);
    }
}