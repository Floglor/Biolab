using Ai;
using Stats;
using Stats.Genetics;
using UnityEngine;

public class GlobalNeedsDecay : MonoBehaviour, INeedsDecay
{
    
    public void NeedsDecayTick(Creature creature)
    {
        creature.reproductionNeed += GlobalValues.Instance.globalBreedDecay;
    }
    public void NeedsDecayTick(GOStatContainer statContainer, CreatureState creatureState)
    {
        throw new System.NotImplementedException();
    }
}