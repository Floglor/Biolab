using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Stats
{
    public class GOStatContainer : MonoBehaviour
    {
        [SerializeField] private ChromosomeStats chromosomeStats; 

        private Dictionary<GeneStat, float> _localStats;
        private Dictionary<GeneStat, float> _originalStats;


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
            _localStats = new Dictionary<GeneStat, float>(chromosomeStats.StatsDictionary);
            _originalStats = new Dictionary<GeneStat, float>(chromosomeStats.StatsDictionary);
            
        }

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            ProceedBuffTimer();
        }

        public void BuffStat(Buff buff)
        {
            bool hasStat = _localStats.TryGetValue(buff.geneStat, out float oldStat);
            
            if (!hasStat) return;

            Buff newBuff = new Buff()
            {
                IsPercentage = buff.IsPercentage,
                IsPermanent = buff.IsPermanent,
                geneStat = buff.geneStat,
                StatValue = buff.StatValue,
                Time = buff.Time
            };

            _buffs.Add(newBuff);

            RecalculateStat(newBuff.geneStat);
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

        private void RecalculateStat(GeneStat geneStat)
        {
            bool hasStat = _originalStats.TryGetValue(geneStat, out float statValue);
            if (!hasStat) return;
            
            float percentageBuffs = 0f;
            float flatBuffs = 0f;
            
            foreach (Buff buff in _buffs)
            {
                if (buff.geneStat != geneStat) continue;

                if (buff.IsPercentage)
                    percentageBuffs += buff.StatValue;
                else
                    flatBuffs += buff.StatValue;
            }

            float resultStat = statValue + flatBuffs;
            resultStat += resultStat * (percentageBuffs / 100f); 
            
            SetStat(geneStat, resultStat);
        }

        private void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
            RecalculateStat(buff.geneStat);
        }


        public float GetStat(GeneStat geneStat)
        {
            if (_localStats.TryGetValue(geneStat, out float value))
            {
                return value;
            }
            else
            {
                Debug.LogError($"No value found for {geneStat} on {name}");
                return 0f;
            }
        }

        public void SetStat(GeneStat geneStat, float changeValue)
        {
            if (_localStats.ContainsKey(geneStat))
            {
                _localStats[geneStat] = changeValue;
            }
            else
            {
                Debug.LogError($"SetStat: No value found for {geneStat} on {name}");
            }
        }

        public bool ContainsKey(GeneStat geneStat)
        {
            return _localStats.ContainsKey(geneStat);
        }
    }
}