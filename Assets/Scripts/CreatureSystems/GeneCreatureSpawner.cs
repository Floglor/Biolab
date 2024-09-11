using System.Collections.Generic;
using Ai;
using Stats;
using Stats.Genetics;
using UnityEngine;

namespace CreatureSystems
{
    public class GeneCreatureSpawner : MonoBehaviour, IMatingCreateNewCreature
    {
        public void CreateCreature(Creature child, Creature mother, Creature father, float mutationModifier)
        {
            child.isMale = Random.Range(0f, 1f) >= 0.5;
            GeneticAlgorithm childGeneticAlgorithm = child.GetComponent<GeneticAlgorithm>();
            GeneticAlgorithm motherGeneticAlgorithm = mother.GetComponent<GeneticAlgorithm>();
            GeneticAlgorithm fatherGeneticAlgorithm = father.GetComponent<GeneticAlgorithm>();

            List<Chromosome> motherGameteChromosomes = motherGeneticAlgorithm.GameteChromosomes;
            List<Chromosome> fatherGameteChromosomes = fatherGeneticAlgorithm.GameteChromosomes;


            List<ChromosomePair> chromosomePairs = new List<ChromosomePair>();

            for (int index = 0; index < motherGameteChromosomes.Count; index++)
            {
                Chromosome motherGameteChromosome = motherGameteChromosomes[index];
                Chromosome fatherGameteChromosome = fatherGameteChromosomes[index];

                chromosomePairs.Add(new ChromosomePair(motherGameteChromosome, fatherGameteChromosome));
            }

            childGeneticAlgorithm.SetParentChromosomes(chromosomePairs);

            float motherCalorieShare = mother.GetStats.GetStat(StatName.ReproductionCalorieShare);

            mother.WeightSystem.LoseCalories(motherCalorieShare);
            child.OnInitialization += () =>
            {
                child.GetStats.AddToStat(StatName.Calories, -child.GetStats.GetStat(StatName.Calories));
                child.GetStats.AddToStat(StatName.Calories, motherCalorieShare);
            };
        }
    }
}