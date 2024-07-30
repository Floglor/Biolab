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
      public Dictionary<GeneStat, float> StatsDictionary = new();
      
      private GeneticAlgorithm _geneticAlgorithm;  


      private void Awake()
      {
         Initialize();
      }

      private void Initialize()
      {
         StatsDictionary = _geneticAlgorithm.GetStats();
      }
      
      public float GetStat(GeneStat geneStat)
      {
         if (StatsDictionary.TryGetValue(geneStat, out float value))
         {
            return value;
         }
         else
         {
            Debug.LogError($"No value found for {geneStat} on {name}");
            return 0f;
         }
      }
   }
}