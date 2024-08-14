using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Stats;
using Stats.Genetics;

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

    private const float CaloriesPerKg = 7700f; // 1 kg of weight ~ 7700 calories
    private const float DailyCaloricExpenditure = 2500f; // Example: 2500 calories/day for maintenance

    public void OnEnable()
    {
        MaxCalorieCount = Weight * CaloriesPerKg;
        MetabolismRatePerSec = DailyCaloricExpenditure / (24 * 3600); // Convert daily expenditure to per-second rate
    }

    public Dictionary<StatName, BaseStat> GetDefaultStats()
    {
        Dictionary<StatName, BaseStat> statsDictionary = new()
        {
            [StatName.Weight] = new SimpleStat(Weight),
            [StatName.Power] = new SimpleStat(Power),
            [StatName.Calories] = new SimpleStat(StartingCalories),
            [StatName.MaxCalorieCount] = new SimpleStat(MaxCalorieCount),
            [StatName.MaxWeight] = new SimpleStat(MaxWeight),
            [StatName.MetabolismRatePerSec] = new SimpleStat(MetabolismRatePerSec)
        };

        statsDictionary[StatName.BaseSpeed] = new DerivedStat(() =>
            BaseSpeedFactor *
            (float)Math.Sqrt(statsDictionary[StatName.Power].Value / statsDictionary[StatName.Weight].Value)
        );

        statsDictionary[StatName.RunSpeed] = new DerivedStat(() =>
            statsDictionary[StatName.BaseSpeed].Value * RunSpeedMultiplier
        );

        statsDictionary[StatName.SprintSpeed] = new DerivedStat(() =>
            statsDictionary[StatName.BaseSpeed].Value * SprintSpeedMultiplier
        );

        statsDictionary[StatName.EnergyConsumptionPerSecond] = new DerivedStat(() =>
            EnergyConsumptionMultiplier *
            ((statsDictionary[StatName.Weight].Value + statsDictionary[StatName.Power].Value) * 0.1f)
        );

        return statsDictionary;
    }
}