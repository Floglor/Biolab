using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Stats;
using Stats.Genetics;

[CreateAssetMenu(menuName = "CreatureDefaultValues")]
public class CreatureDefaultValues : SerializedScriptableObject
{
    [FoldoutGroup("Simple Stats")]
    public float DefaultMass = 15f;

    [FoldoutGroup("Simple Stats")]
    public float DefaultPower = 15f;

    [FoldoutGroup("Max Stats")]
    public float DefaultStaminaMax = 100f;

    [FoldoutGroup("Derived Stats")]
    public float DefaultSpeed = 1f;

    public Dictionary<StatName, BaseStat> GetDefaultStats()
    {
        var statsDictionary = new Dictionary<StatName, BaseStat>();

        statsDictionary[StatName.Mass] = new SimpleStat(DefaultMass);
        statsDictionary[StatName.Power] = new SimpleStat(DefaultPower);

        statsDictionary[StatName.Stamina] = new StatWithMax(DefaultStaminaMax, DefaultStaminaMax);

        statsDictionary[StatName.Speed] = new DerivedStat(() =>
                statsDictionary[StatName.Power].Value / statsDictionary[StatName.Mass].Value
            );

        return statsDictionary;
    }
}