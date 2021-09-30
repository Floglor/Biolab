using UnityEngine;

public class GlobalNeedsDecay : MonoBehaviour, INeedsDecay
{
    public void NeedsDecayTick(Creature creature)
    {
        creature.thirst += GlobalValues.Instance.globalThirstDecay;
        if (creature.isRunning) creature.thirst += GlobalValues.Instance.runningThirstDecay;

        creature.hunger += GlobalValues.Instance.globalHungerDecay;
        creature.reproductionNeed += GlobalValues.Instance.globalBreedDecay;
    }
}