using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Stats.Genetics;
using UnityEngine;

namespace Stats
{
   [RequireComponent(typeof(GeneticAlgorithm))]
   public class ChromosomeStats : MonoBehaviour
   {
      public Dictionary<StatName, BaseStat> StatsDictionary = new();
      
      private GeneticAlgorithm _geneticAlgorithm;  


      private void Awake()
      {
         Initialize();
      }

      private void Initialize()
      {
         _geneticAlgorithm = GetComponent<GeneticAlgorithm>();
         StatsDictionary = _geneticAlgorithm.GetStats();
      }
   }
}