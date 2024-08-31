using System;
using Ai;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CreatureSystems
{
    public class DefaultCreatureSpawner : MonoBehaviour, IMatingCreateNewCreature
    {
        public void CreateCreature(Creature child, Creature mother, Creature father, float mutationModifier)
        {
            child.startEyesight = FindStatWithMutation(mother.startEyesight, father.startEyesight, mutationModifier);
            //child.startSize = FindStatWithMutation(mother.startSize, father.startSize, mutationModifier);
            child.startEatingSpeed =
                FindStatWithMutation(mother.startEatingSpeed, father.startEatingSpeed, mutationModifier);
            //child.Speed = FindStatWithMutation(mother.startSpeed, father.startSpeed, mutationModifier);
            child.isMale = Random.Range(0f, 1f) >= 0.5;
        }

        private int FindStatWithMutation(int motherStat, int fatherStat, float mutationModifier)
        {
            return (int) Random.Range(Math.Min(motherStat, fatherStat) - mutationModifier,
                Math.Max(motherStat, fatherStat) + mutationModifier);
        }

        private static float FindStatWithMutation(float motherStat, float fatherStat, float mutationModifier)
        {
            return Random.Range(Math.Min(motherStat, fatherStat) - mutationModifier,
                Math.Max(motherStat, fatherStat) + mutationModifier);
        }
    }
}