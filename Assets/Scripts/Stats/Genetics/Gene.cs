using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Stats.Genetics
{
    [CreateAssetMenu(menuName = "Gene")]

    public class Gene : SerializedScriptableObject
    {
        public List<Base> Bases;
        public bool IsDominant;
        public GeneCode geneCode;
    }
}