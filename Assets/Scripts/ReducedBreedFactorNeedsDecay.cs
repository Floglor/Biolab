using Ai;
using Stats;
using UnityEngine;

public class ReducedBreedFactorNeedsDecay : MonoBehaviour, INeedsDecay
{
        
    public void NeedsDecayTick(Creature creature)
    {
        creature.reproductionNeed += GlobalValues.Instance.globalBreedDecay/3;   
    }
    

    public void NeedsDecayTick(GOStatContainer statContainer, CreatureState creatureState)
    {
        throw new System.NotImplementedException();
    }
}