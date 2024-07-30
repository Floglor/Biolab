using UnityEngine;

public interface IRepeatMove
{
    void StopRepeatMove();
    void StartRepeatMove(Creature creature, Transform transformToFollow);
}