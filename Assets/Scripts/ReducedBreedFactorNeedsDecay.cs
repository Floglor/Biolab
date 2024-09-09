using Ai;
using CreatureSystems;
using Stats;
using UnityEngine;

public class ReducedBreedFactorNeedsDecay : MonoBehaviour, INeedsDecay
{
        
    public void NeedsDecayTick(Creature creature)
    {
    }
    

    public void NeedsDecayTick(GOStatContainer statContainer, CreatureState creatureState)
    {
        throw new System.NotImplementedException();
    }
}