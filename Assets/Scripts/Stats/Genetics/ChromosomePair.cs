using System;

namespace Stats.Genetics
{
    [Serializable]
    public class ChromosomePair
    {
        public Chromosome MotherChromosome;
        public Chromosome FatherChromosome;

        public ChromosomePair(Chromosome motherChromosome, Chromosome fatherChromosome)
        {
            MotherChromosome = motherChromosome;
            FatherChromosome = fatherChromosome;
        }
    }
}