using System.Collections.Generic;
using Stats.Genetics;
using UnityEngine;

namespace Stats
{
    public class GOStatContainer : MonoBehaviour
    {
        [SerializeField] private GeneticAlgorithm geneticAlgorithm;

        private Dictionary<StatName, BaseStat> _localStats;
        private Dictionary<StatName, BaseStat> _originalStats;


        private void Initialize()
        {
            _localStats = geneticAlgorithm.GeneStats;
            _originalStats = new Dictionary<StatName, BaseStat>(geneticAlgorithm.GeneStats);
        }

        private void Start()
        {
            Initialize();
        }


        public float GetStat(StatName statName)
        {
            if (_localStats.TryGetValue(statName, out BaseStat value))
            {
                return value.Value;
            }
            else
            {
                Debug.LogError($"No value found for {statName} on {name}");
                return 0f;
            }
        }

        public void AddToStat(StatName statName, float changeValue)
        {
            if (_localStats.ContainsKey(statName))
            {
                _localStats[statName].UpdateBaseStat(changeValue);
            }
            else
            {
                Debug.LogError($"AddToStat: No value found for {statName} on {name}");
            }
        }

        public bool ContainsKey(StatName statName)
        {
            return _localStats.ContainsKey(statName);
        }
    }
}