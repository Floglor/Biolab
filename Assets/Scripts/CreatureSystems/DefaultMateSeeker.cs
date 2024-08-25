using System;
using System.Collections.Generic;
using System.Linq;
using Ai;
using ModestTree;
using Sirenix.Utilities;
using UnityEngine;

internal class DefaultMateSeeker : MonoBehaviour, IMateSeeker
{
    private CreatureList _creatureList;

    private void Start()
    {
        _creatureList = CreatureList.Instance;
    }


    public Creature SeekMate(Creature creature)
    {
        if (creature.stateController.timersHandler
            .GetOrCreateTimer(TimerName.Mating, GlobalValues.Instance.searchMateDelay).TimerIsRunning) return null;

        List<Creature> sortedCreatureList = ReturnSingleSexAndUnoccupied(!creature.isMale,
            _creatureList.SortByDistance(creature.transform.position), creature);

        if (sortedCreatureList.IsNullOrEmpty())
        {
            creature.stateController.timersHandler.ResetTimer(TimerName.Mating);
            Debug.Log($"Creature {creature.name} can't find a mate");
            return null;
        }

        Creature maxReproductionNeedCreature = SeekMAXReproductionNeedCreature(sortedCreatureList);
        creature.stateController.timersHandler.ResetTimer(TimerName.Mating);
        return maxReproductionNeedCreature;
    }

    public void BeTargeted(Creature targetCreature, Creature targetingCreature)
    {
        if (!(targetCreature.hunger <= GlobalValues.Instance.basicNeedHighThreshold) ||
            !(targetCreature.thirst <= GlobalValues.Instance.basicNeedHighThreshold)) return;

        targetCreature.stateController.lastTargetedMate = targetingCreature;
        targetCreature.stateController.targetCreature = targetingCreature;
        targetCreature.stateController.SetMatingState();
    }

    private List<Creature> ReturnSingleSexAndUnoccupied(bool reversedSex, List<Creature> creatures,
        Creature searchingCreature)
    {
        if (creatures.IsNullOrEmpty()) return null;
        if (searchingCreature == null)
        {
            Debug.Log("searching creature is null!");
            return null;
        }

        List<Creature> returnCreatureList = new List<Creature>();
        foreach (Creature creature in creatures)
        {
            if (creature != null)
            {
                if (creature.isMale == reversedSex)
                {
                    if (creature.stateController.lastTargetedMate == null)
                    {
                        if (creature.thirst <= GlobalValues.Instance.basicNeedHighThreshold)
                        {
                            if (creature.hunger <= GlobalValues.Instance.basicNeedHighThreshold)
                            {
                                if (creature.stateController.currentState == creature.stateController.initialState ||
                                    creature.stateController.currentState == creature.stateController.lookForMateState)
                                {
                                    if (creature.creatureType == searchingCreature.creatureType)
                                        returnCreatureList.Add(creature);
                                }
                            }
                        }
                    }
                }
            }
        }


        return returnCreatureList.IsNullOrEmpty() ? null : returnCreatureList;
    }


    private static Creature SeekMAXReproductionNeedCreature(List<Creature> sortedCreatureList)
    {
        Creature maxReproductionNeedCreature = null;
        float maxReproductionNeed = Mathf.NegativeInfinity;

        foreach (Creature sortingCreature in sortedCreatureList.Where(sortingCreature =>
                     sortingCreature.reproductionNeed > maxReproductionNeed))
        {
            maxReproductionNeed = sortingCreature.reproductionNeed;
            maxReproductionNeedCreature = sortingCreature;
        }

        return maxReproductionNeedCreature;
    }
}