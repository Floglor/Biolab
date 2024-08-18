using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Stats.Genetics
{
    [Serializable]
    internal class TestOdinEntry
    {
        [FormerlySerializedAs("geneStat")] public StatName statName;
        public BaseStat value;

        public TestOdinEntry(StatName statName, BaseStat value)
        {
            this.statName = statName;
            this.value = value;
        }
    }


    public class GeneticAlgorithm : MonoBehaviour
    {
        [SerializeField] private List<ChromosomePair> parentChromosomes;

        [SerializeField] private List<Chromosome> gameteChromosomes;

        [SerializeField] private Dictionary<StatName, BaseStat> _testingStats;
        [SerializeField] private List<TestOdinEntry> _testEntries;

        private Dictionary<StatName, BaseStat> _geneStats;

        public Dictionary<StatName, BaseStat> GeneStats => _geneStats;

        public CreatureDefaultValues CreatureDefaultValues;

        private void Awake()
        {
            _geneStats = CreatureDefaultValues.GetDefaultStats();
            float deltaTime = 1;
        }

        [Button]
        private void TestStats()
        {
            _testingStats = GetStats();

            _testEntries = new List<TestOdinEntry>();

            foreach (KeyValuePair<StatName, BaseStat> keyValuePair in _testingStats)
            {
                _testEntries.Add(new TestOdinEntry(keyValuePair.Key, keyValuePair.Value));
            }
        }


        public Dictionary<StatName, BaseStat> GetStats()
        {
            Dictionary<StatName, BaseStat> returnStats = new Dictionary<StatName, BaseStat>();

            foreach (ChromosomePair parentChromosome in parentChromosomes)
            {
                for (int i = 0; i < parentChromosome.FatherChromosome.Genes.Count; i++)
                {
                    Gene currentGeneFather = parentChromosome.FatherChromosome.Genes[i];
                    Gene currentGeneMother = parentChromosome.MotherChromosome.Genes[i];

                    if (currentGeneFather.IsDominant && !currentGeneMother.IsDominant)
                    {
                        WriteBases(returnStats, currentGeneFather);
                    }
                    else if (currentGeneMother.IsDominant && !currentGeneFather.IsDominant)
                    {
                        WriteBases(returnStats, currentGeneMother);
                    }
                    else if (currentGeneFather.IsDominant && currentGeneMother.IsDominant ||
                             !currentGeneFather.IsDominant && !currentGeneMother.IsDominant)
                    {
                        WriteBases(returnStats, currentGeneFather, 2);
                        WriteBases(returnStats, currentGeneMother, 2);
                    }
                }
            }

            return returnStats;
        }

        public static void WriteBases(Dictionary<StatName, BaseStat> stats, Gene gene, int divider = 1)
        {
            foreach (Base geneBase in gene.Bases)
            {
                if (stats.ContainsKey(geneBase.statName))
                {
                    stats[geneBase.statName].UpdateBaseStat(geneBase.Value / divider);
                }
                else
                {
                    stats[geneBase.statName] = new SimpleStat(geneBase.Value / divider);
                }
            }
        }

        [Button]
        private void GameteMeiosis()
        {
            gameteChromosomes = Meiosis(parentChromosomes);
        }

        [SerializeField] public ChromosomePair testChromosomePair;


        private static void Crossover(Chromosome chromosome1, Chromosome chromosome2)
        {
            int length = chromosome1.Genes.Count;
            int crossoverPoint = UnityEngine.Random.Range(0, length);
            int crossoverEndPoint = UnityEngine.Random.Range(crossoverPoint, length);

            // Ensure chromosomes are not null and have the same length
            if (chromosome1.Genes.Count == chromosome2.Genes.Count && chromosome1.Genes.Count > 1)
            {
                for (int i = crossoverPoint; i <= crossoverEndPoint; i++)
                {
                    (chromosome1.Genes[i], chromosome2.Genes[i]) = (chromosome2.Genes[i], chromosome1.Genes[i]);
                }
            }
        }

        private static List<Chromosome> Meiosis(List<ChromosomePair> parentChromosomePairs)
        {
            foreach (ChromosomePair parentChromosomePair in parentChromosomePairs)
            {
                EnsureGeneCodeConsistency(parentChromosomePair);
            }

            List<Chromosome> gameteChromosomes = new List<Chromosome>();

            foreach (ChromosomePair parentChromosomePair in parentChromosomePairs)
            {
                Chromosome fatherChromosomeCopy = CreateChromosomeCopy(parentChromosomePair.FatherChromosome);
                Chromosome motherChromosomeCopy = CreateChromosomeCopy(parentChromosomePair.MotherChromosome);

                Crossover(fatherChromosomeCopy, motherChromosomeCopy);

                gameteChromosomes.Add(Random.Range(0f, 1f) < 0.5f ? fatherChromosomeCopy : motherChromosomeCopy);
            }

            return gameteChromosomes;
        }

        private static void EnsureGeneCodeConsistency(ChromosomePair chromosomePair)
        {
            int fatherGeneCount = chromosomePair.FatherChromosome.Genes.Count;
            int motherGeneCount = chromosomePair.MotherChromosome.Genes.Count;

            if (fatherGeneCount != motherGeneCount)
            {
                int minCount = Mathf.Min(fatherGeneCount, motherGeneCount);
                chromosomePair.FatherChromosome.Genes = chromosomePair.FatherChromosome.Genes.Take(minCount).ToList();
                chromosomePair.MotherChromosome.Genes = chromosomePair.MotherChromosome.Genes.Take(minCount).ToList();
            }

            for (int i = 0; i < chromosomePair.FatherChromosome.Genes.Count; i++)
            {
                Gene fatherGene = chromosomePair.FatherChromosome.Genes[i];
                Gene motherGene = chromosomePair.MotherChromosome.Genes[i];

                if (fatherGene.geneCode != motherGene.geneCode)
                {
                    Gene matchingMotherGene = null;
                    foreach (Gene gene in chromosomePair.MotherChromosome.Genes)
                    {
                        if (gene.geneCode == fatherGene.geneCode)
                        {
                            matchingMotherGene = gene;
                            break;
                        }
                    }

                    if (matchingMotherGene != null)
                    {
                        chromosomePair.MotherChromosome.Genes[i] = matchingMotherGene;
                    }
                    else
                    {
                        Gene matchingFatherGene = null;
                        foreach (Gene gene in chromosomePair.FatherChromosome.Genes)
                        {
                            if (gene.geneCode == motherGene.geneCode)
                            {
                                matchingFatherGene = gene;
                                break;
                            }
                        }

                        if (matchingFatherGene != null)
                        {
                            chromosomePair.FatherChromosome.Genes[i] = matchingFatherGene;
                        }
                    }
                }
            }
        }

        private static Chromosome CreateChromosomeCopy(Chromosome original)
        {
            Chromosome copy = new Chromosome
            {
                index = original.index,
                Genes = new List<Gene>()
            };

            foreach (Gene gene in original.Genes)
            {
                copy.Genes.Add(gene);
            }

            return copy;
        }
    }
}