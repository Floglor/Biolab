using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "CreatureDefaultValues")]
    public class CreatureDefaultValues : SerializedScriptableObject
    {
        public float Weight = 70f;
        public float MaxWeight = 120f;
        public float Power = 70f;
        public float BaseSpeedFactor = 2.0f;
        public float RunSpeedMultiplier = 1.0f;
        public float SprintSpeedMultiplier = 1.5f;
        public float EnergyConsumptionMultiplier = 1.0f;
        public float StartingCalories = 20000f;
        public float MaxCalorieCount;
        public float MetabolismRatePerSec;
        public float Hunger = 0f;
        public float MaxHunger = 100f;
        public float Thirst = 0f;
        public float MaxThirst = 100f;
        public float HungerDecay = 0.5f;
        public float CaloriesToHungerConversionRate = 1;
        public float HungerDC = 14f;
        public float HungerDecayInterval = 60f;
        public float ReproductionNeed = 0;
        public float MaxReproductionNeed = 100f;
        public float ReproductionNeedDecay = 0.2f;
        public float PerfectCalorieCount = 200f;
        public float ReproductionCalorieShare = 40f;

        private const float CaloriesPerKg = 77f; // 1 kg of weight ~ 7700 calories
        private const float DailyCaloricExpenditure = 25f; // Example: 2500 calories/day for maintenance

     // public void OnEnable()
     // {
     //     MaxCalorieCount = Weight * CaloriesPerKg;
     //     MetabolismRatePerSec =
     //         DailyCaloricExpenditure / (24 * 3600); // Convert daily expenditure to per-second rate
     // }

        public Dictionary<StatName, BaseStat> GetDefaultStats()
        {
            Dictionary<StatName, BaseStat> statsDictionary = new()
            {
                [StatName.Weight] = new SimpleStat(Weight),
                [StatName.Power] = new SimpleStat(Power),
                [StatName.Calories] = new SimpleStat(StartingCalories),
                [StatName.MaxCalorieCount] = new SimpleStat(MaxCalorieCount),
                [StatName.MaxWeight] = new SimpleStat(MaxWeight),
                [StatName.MetabolismRatePerSec] = new SimpleStat(MetabolismRatePerSec),
                [StatName.HungerDecay] = new SimpleStat(HungerDecay),
                [StatName.CaloriesToHungerConversionRate] = new SimpleStat(CaloriesToHungerConversionRate),
                [StatName.HungerDC] = new SimpleStat(HungerDC),
                [StatName.HungerDecayInterval] = new SimpleStat(HungerDecayInterval),
                [StatName.ReproductionNeedDecay] = new SimpleStat(ReproductionNeedDecay),
                [StatName.PerfectCalorieCount] = new SimpleStat(PerfectCalorieCount),
                [StatName.ReproductionCalorieShare] = new SimpleStat(ReproductionCalorieShare),

            };

            statsDictionary[StatName.BaseSpeed] = new DerivedStat(() =>
                BaseSpeedFactor *
                (float) Math.Sqrt(statsDictionary[StatName.Power].Value / statsDictionary[StatName.Weight].Value)
            );

            statsDictionary[StatName.RunSpeed] = new DerivedStat(() =>
                statsDictionary[StatName.BaseSpeed].Value * RunSpeedMultiplier
            );

            statsDictionary[StatName.SprintSpeed] = new DerivedStat(() =>
                statsDictionary[StatName.BaseSpeed].Value * SprintSpeedMultiplier
            );

            statsDictionary[StatName.EnergyConsumptionPerSecond] = new DerivedStat(() =>
                EnergyConsumptionMultiplier *
                ((statsDictionary[StatName.Weight].Value + statsDictionary[StatName.Power].Value) * 0.001f)
            );

            statsDictionary[StatName.Hunger] = new StatWithMax(Hunger, MaxHunger);
            statsDictionary[StatName.Thirst] = new StatWithMax(Thirst, MaxThirst);
            statsDictionary[StatName.ReproductionNeed] = new StatWithMax(ReproductionNeed, MaxReproductionNeed);


            return statsDictionary;
        }
    }
}