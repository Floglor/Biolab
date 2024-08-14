using Ai;
using Stats;
using UnityEngine;

public class ReducedBreedFactorNeedsDecay : MonoBehaviour, INeedsDecay
{
        
    public void NeedsDecayTick(Creature creature)
    {
        creature.thirst += GlobalValues.Instance.globalThirstDecay;
        if (creature.isRunning) creature.thirst += GlobalValues.Instance.runningThirstDecay;

        creature.hunger += GlobalValues.Instance.globalHungerDecay;
            
        creature.reproductionNeed += GlobalValues.Instance.globalBreedDecay/3;   
    }
    

    public void NeedsDecayTick(GOStatContainer statContainer, CreatureState creatureState)
    {
        throw new System.NotImplementedException();
    }
}