﻿using System;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;

namespace CreatureSystems
{
    [RequireComponent(typeof(Creature))]
    public class HungerSystem : MonoBehaviour, IHungerSystem, IReceiveDeathAction, IReceiveStatContainer
    {
        private const float MAX_HUNGER = 100f;
        private GOStatContainer _statContainer;
        private Action<DeathReason> _deathAction;

        [ShowInInspector] [ReadOnly] private float _timeSinceLastDecay;
        [ShowInInspector] [ReadOnly] private int _hungerDC;
        private float _decayInterval;

        [ShowInInspector] [ReadOnly] private float _hungerDebug;
        [ShowInInspector] [ReadOnly] private float _thirstDebug;

        private IWeightSystem _weightSystem;


        private void Start()
        {
            _hungerDC = (int) _statContainer.GetStat(StatName.HungerDC);
            _decayInterval = _statContainer.GetStat(StatName.HungerDecayInterval);
            _weightSystem = GetComponent<Creature>().WeightSystem;
        }

        private void Update()
        {
            HungerDecay();
            _hungerDebug = _statContainer.GetStat(StatName.Hunger);
            _thirstDebug = _statContainer.GetStat(StatName.Thirst);
        }

        public void HungerDecay()
        {
            float nourishment = _weightSystem.ReturnNourishment();

            float multiplier = 1 + 30 * Mathf.Pow((100 - nourishment) / 100, 2);
            
            
            float hunger = _statContainer.GetStat(StatName.Hunger);
            float thirst = _statContainer.GetStat(StatName.Thirst);
            
            

            if (hunger <= MAX_HUNGER)
            {
                _statContainer.AddToStat(StatName.Hunger,
                    _statContainer.GetStat(StatName.HungerDecay) * (Time.deltaTime * multiplier));
            }

            if (thirst <= MAX_HUNGER)
            {
                _statContainer.AddToStat(StatName.Thirst,
                    _statContainer.GetStat(StatName.HungerDecay) * (Time.deltaTime));
            }

            _timeSinceLastDecay += Time.deltaTime;

            if ((hunger >= MAX_HUNGER || thirst >= MAX_HUNGER) && _timeSinceLastDecay >= _decayInterval)
            {
                PerformDeathCheck();
                _statContainer.AddToStat(StatName.Hunger, -_statContainer.GetStat(StatName.Hunger));
                _hungerDC = Math.Max(1, _hungerDC - 1);
                _timeSinceLastDecay = 0f;
            }
        }

        public void PerformDeathCheck()
        {
            if (_statContainer.GetStat(StatName.Hunger) >= MAX_HUNGER ||
                _statContainer.GetStat(StatName.Thirst) >= MAX_HUNGER)
            {
                int roll = Roll3d6();

                if (roll > _hungerDC)
                {
                    _deathAction?.Invoke(_statContainer.GetStat(StatName.Hunger) >= MAX_HUNGER
                        ? DeathReason.Hunger
                        : DeathReason.Thirst);
                }
            }
        }

        public void SatisfyHunger(float calories)
        {
            _statContainer.AddToStat(StatName.Hunger,
                -(calories * _statContainer.GetStat(StatName.CaloriesToHungerConversionRate)));
        }

        public void SatisfyThirst(float amount)
        {
            _statContainer.AddToStat(StatName.Thirst,
                -amount);
        }

        public float GetHunger()
        {
            return _statContainer.GetStat(StatName.Hunger);
        }

        public float GetThirst()
        {
            return _statContainer.GetStat(StatName.Thirst);
        }

        public void SetDeathAction(Action<DeathReason> deathAction)
        {
            _deathAction = deathAction;
        }

        public void SetStatContainer(GOStatContainer statContainer)
        {
            _statContainer = statContainer;
        }

        private int Roll3d6()
        {
            return UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7);
        }
    }
}