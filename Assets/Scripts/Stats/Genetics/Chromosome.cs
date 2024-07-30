using System;
using System.Collections.Generic;

namespace Stats.Genetics
{
    [Serializable]
    public class Chromosome
    {
        public int index;
        public List<Gene> Genes;
    }
}