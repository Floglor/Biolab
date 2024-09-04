using System;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;

namespace CreatureSystems
{
    [RequireComponent(typeof(Creature))]
    public class WeightSystem : MonoBehaviour
    {
        private GOStatContainer _statContainer;

        [ShowInInspector] [ReadOnly] private bool _isGainingWeight = false;
        [ShowInInspector] [ReadOnly] private bool _isLosingWeight = false;

        private bool _isDeathEventFired = false;

        [ShowInInspector] [ReadOnly] private float _weight;
        [ShowInInspector] [ReadOnly] private float _calories;
        [ShowInInspector] [ReadOnly] private float _energyConsumptionPerSecond;

        private Action<DeathReason> _deathAction;

        [Button]
        private void SetCaloriesToZero()
        {
            _statContainer.AddToStat(StatName.Calories, -_statContainer.GetStat(StatName.Calories));
        }

        private void GainWeight(float weight)
        {
            if (_isGainingWeight)
                _statContainer.AddToStat(StatName.Weight, weight / GlobalValues.Instance.weightToCalorieRatio);
        }

        private void TransferWeightToCalories(float amount)
        {
            if (_isLosingWeight)
            {
                _statContainer.AddToStat(StatName.Calories, amount * GlobalValues.Instance.weightToCalorieRatio);
                _statContainer.AddToStat(StatName.Weight, -amount);


                if (_statContainer.GetStat(StatName.Weight) < _statContainer.GetStat(StatName.MaxWeight) *
                    GlobalValues.Instance.minimumWeightThreshold || _isDeathEventFired)
                {
                    Debug.Log($"creature {gameObject.name} is below the minimum weight!");
                    _deathAction.Invoke(DeathReason.WeightLoss);
                    _isDeathEventFired = true;
                }
            }
        }

        private void Update()
        {
            if (_isGainingWeight)
            {
                GainWeight(_statContainer.GetStat(StatName.MetabolismRatePerSec) * Time.deltaTime);
            }

            if (_isLosingWeight)
            {
                TransferWeightToCalories(_statContainer.GetStat(StatName.MetabolismRatePerSec) * Time.deltaTime);
            }

            LoseCalories();

            _isLosingWeight = _statContainer.GetStat(StatName.Calories) < 0;

            UpdateDebugStats();
        }

        private void UpdateDebugStats()
        {
            _weight = _statContainer.GetStat(StatName.Weight);
            _calories = _statContainer.GetStat(StatName.Calories);
        }

        private void LoseCalories()
        {
            _statContainer.AddToStat(StatName.Calories,
                -_statContainer.GetStat(StatName.EnergyConsumptionPerSecond) * Time.deltaTime);
            
            _energyConsumptionPerSecond = _statContainer.GetStat(StatName.EnergyConsumptionPerSecond);
        }

        private void Start()
        {
            _statContainer = GetComponent<Creature>().GetStats;
            _deathAction = GetComponent<Creature>().DeathAction;
        }

        private void HandleExcessCalories()
        {
            _isGainingWeight = _statContainer.GetStat(StatName.Calories) >
                               _statContainer.GetStat(StatName.MaxCalorieCount);
        }


        public void GainCalories(float calories)
        {
            _statContainer.AddToStat(StatName.Calories, calories);
            HandleExcessCalories();
        }
    }
}