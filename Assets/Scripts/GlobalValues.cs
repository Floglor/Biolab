using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalValues : MonoBehaviour
{
    public static GlobalValues Instance;

    [BoxGroup("Needs Decay")] public float globalThirstDecay;

    [BoxGroup("Needs Decay")] public float globalHungerDecay;

    [BoxGroup("Needs Decay")] public float globalBreedDecay;

    [BoxGroup("Distances")] public float closeDistance;


    [BoxGroup("Food Sprites")] public Sprite emptyFoodSprite;
    [BoxGroup("Food Sprites")] public Sprite fullFoodSprite;
    [BoxGroup("Food Sprites")] public Sprite halfFoodSprite;
    [BoxGroup("Food Sprites")] public Sprite quarterFoodSprite;

    [BoxGroup("Search Delays")] public float searchFoodDelay;
    [BoxGroup("Search Delays")] public float searchWaterDelay;
    [BoxGroup("Search Delays")] public float searchMateDelay;
    [BoxGroup("OtherDelays")] public float predatorAwarenessDelay;

    [BoxGroup("OtherDelays")] public float repeatMoveDelay;
    [BoxGroup("OtherDelays")] public float iconResetDelay;
    [BoxGroup("OtherDelays")] public float huntCooldown;
    [BoxGroup("OtherDelays")] public float calmDownFromChaseDelay;

    [BoxGroup("Needs Satisfaction")] public float basicNeedHighThreshold;
    [BoxGroup("Needs Satisfaction")] public float basicNeedLowThreshold;
    [FormerlySerializedAs("matingNeedHighTreshold")] [BoxGroup("Needs Satisfaction")] public float matingNeedHighThreshold;
    [BoxGroup("Needs Satisfaction")] public float maxHungerDeathThreshold;
    [BoxGroup("Needs Satisfaction")] public float maxThirstDeathThreshold;
    [BoxGroup("Needs Satisfaction")] public float minHuntDecisionHungerThreshold;
    [BoxGroup("Needs Satisfaction")] public float lowNourishmentThreshold;

    [BoxGroup("Weight")] public float minimumWeightThreshold;
    [BoxGroup("Weight")] public float weightToCalorieRatio;


    [BoxGroup("Other")] public float corpseMaxAge;
    [BoxGroup("Other")] public float runningThirstDecay;
    [BoxGroup("Other")] public float runningSpeedMultiplier;
    [BoxGroup("Other")] public int MaxCreaturesOnTile;
    [BoxGroup("Other")] public float corpseSizeMultiplier;

    public GameObject debugObj;


    private void Awake()
    {
        Instance = this;
    }
}