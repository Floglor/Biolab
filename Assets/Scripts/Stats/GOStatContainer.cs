using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Stats.Genetics;
using UnityEngine;

namespace Stats
{
    public class GOStatContainer : MonoBehaviour
    {
        [SerializeField] private GeneticAlgorithm geneticAlgorithm;

        private Dictionary<StatName, BaseStat> _localStats;
        private Dictionary<StatName, BaseStat> _originalStats;


        private List<Buff> _buffs;

        //testing
        public Buff TargetBuff;

        [Button]
        public void AddTargetBuff()
        {
            BuffStat(TargetBuff);
        }

        private void Initialize()
        {
            _localStats = geneticAlgorithm.GeneStats;
            _originalStats = new Dictionary<StatName, BaseStat>(geneticAlgorithm.GeneStats);
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            ProceedBuffTimer();
        }

        public void BuffStat(Buff buff)
        {
            bool hasStat = _localStats.TryGetValue(buff.statName, out BaseStat oldStat);

            if (!hasStat) return;

            Buff newBuff = new Buff()
            {
                IsPercentage = buff.IsPercentage,
                IsPermanent = buff.IsPermanent,
                statName = buff.statName,
                StatValue = buff.StatValue,
                Time = buff.Time
            };

            _buffs.Add(newBuff);

            RecalculateStat(newBuff.statName);
        }


        private void ProceedBuffTimer()
        {
            if (_buffs.IsNullOrEmpty()) return;

            for (int i = 0; i < _buffs.Count; i++)
            {
                Buff buff = _buffs[i];

                if (buff.IsPermanent) continue;

                buff.Time -= Time.deltaTime;

                if (buff.Time <= 0f)
                {
                    RemoveBuff(buff);
                }
            }
        }

        private void RecalculateStat(StatName statName)
        {
            bool hasStat = _originalStats.TryGetValue(statName, out BaseStat statValue);
            if (!hasStat) return;

            float percentageBuffs = 0f;
            float flatBuffs = 0f;

            foreach (Buff buff in _buffs)
            {
                if (buff.statName != statName) continue;

                if (buff.IsPercentage)
                    percentageBuffs += buff.StatValue;
                else
                    flatBuffs += buff.StatValue;
            }

            float resultStat = statValue.Value + flatBuffs;
            resultStat += resultStat * (percentageBuffs / 100f);

            SetStat(statName, resultStat);
        }

        private void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
            RecalculateStat(buff.statName);
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

        public void SetStat(StatName statName, float changeValue)
        {
            if (_localStats.ContainsKey(statName))
            {
                if (_localStats[statName] is SimpleStat simpleStat)
                    simpleStat.UpdateBaseStat(changeValue);
            }
            else
            {
                Debug.LogError($"SetStat: No value found for {statName} on {name}");
            }
        }

        public bool ContainsKey(StatName statName)
        {
            return _localStats.ContainsKey(statName);
        }
    }
}