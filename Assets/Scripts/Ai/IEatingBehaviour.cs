public interface IEatingBehaviour
{
    void StartDrinking(Creature creature);
    void StartEating(Creature creature);
    void CancelEating(Creature creature);
    void CancelDrinking(Creature creature);
}