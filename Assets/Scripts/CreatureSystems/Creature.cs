using System;
using Ai;
using Pathfinding;
using Pathfinding.RVO;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Creature : MonoBehaviour
{
    private const int HiddenAlpha = 50;
    private const int NotHiddenAlpha = 255;

    public const float EatingRange = 3f;
    public const float ChompSize = 0.5f;
    public string speciesID;
    public CreatureType creatureType;

    public float startHunger;
    [ShowInInspector] public float hunger;
    public float maxHunger;

    public float startThirst;
    [ShowInInspector] public float thirst;
    public float maxThirst;

    public float startReproductionNeed;
    public float reproductionNeed;
    

    public float startEyesight;
    public float eyesight;

    public float startEatingSpeed;
    public float eatingSpeed;

    public bool isMale;
    public bool isHidden;
    public bool isHerbivore;
    public bool isRunning;
    public bool isAlert;

    public AIPath aiPath;
    public Seeker seeker;
    public Vector3 lastFoodPosition;
    public Vector3 lastWaterPosition;

    public Action onDrinkingFinsihed;

    public StateController stateController;
    public bool alreadyEating;
    public bool alreadyDrinking;

    [HideInInspector] public RVOController rvoController;
    public Corpse lastCorpse;
    private INeedsDecay _needsDecayBehaviour;
    private IPredatorAwareness _predatorAwareness;
    private INeedsUI _needsUI;

    private float _speed;


    // ReSharper disable once UnusedMember.Global
    public IMateSeeker AISeeker;


    public IEatingBehaviour EatingBehaviour;

    public CustomTile LastFoodTile;
    public IMovingBehaviour MovingBehaviour;
    public IRepeatMove RepeatMoveBehaviour;
    public CreatureState state;

    public Action DeathAction;

    public float Speed
    {
        private set
        {
            aiPath.maxSpeed = value;
            _speed = value;
        }
        get => _speed;
    }

    public GOStatContainer GetStats { get; private set; }

    protected void Start()
    {
        GetStats = GetComponent<GOStatContainer>();
        aiPath = GetComponent<AIPath>();
        seeker = GetComponent<Seeker>();
        EatingBehaviour = GetComponent<IEatingBehaviour>();
        _needsDecayBehaviour = GetComponent<INeedsDecay>();
        rvoController = GetComponent<RVOController>();
        MovingBehaviour = GetComponent<IMovingBehaviour>();
        RepeatMoveBehaviour = GetComponent<IRepeatMove>();
        _needsUI = GetComponent<INeedsUI>();
        stateController = GetComponent<StateController>();

        DeathAction += Die;
        

        InitializeStartingStats();
        aiPath.maxSpeed = Speed;

        InvokeRepeating(nameof(NeedsDecay), 0, 0.5f);
        if (isHerbivore)
        {
            _predatorAwareness = GetComponent<IPredatorAwareness>();
            StartCoroutine(_predatorAwareness.BeAware(this));
        }
    }

    private void OnDestroy()
    {
        CreatureList.Instance.allCreatures.Remove(this);
    }

    public void Hide()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = HiddenAlpha;

        GetComponent<SpriteRenderer>().color = color;

        isHidden = true;
    }

    public void ShowYourself()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = NotHiddenAlpha;

        GetComponent<SpriteRenderer>().color = color;

        isHidden = false;
    }


    private void InitializeStartingStats()
    {
        CreatureList.Instance.allCreatures.Add(this);
        Speed = GetStats.GetStat(StatName.BaseSpeed);
        eyesight = startEyesight;
        hunger = startHunger + Random.Range(5f, 10f);
        thirst = startThirst + Random.Range(5f, 10f);
        reproductionNeed = startReproductionNeed + Random.Range(5f, 10f);
        aiPath.maxSpeed = Speed;
        eatingSpeed = startEatingSpeed;
        alreadyEating = false;
        maxHunger = GlobalValues.Instance.maxHungerDeathThreshold;
        maxThirst = GlobalValues.Instance.maxThirstDeathThreshold;
    }


    protected void NeedsDecay()
    {
        _needsDecayBehaviour.NeedsDecayTick(GetStats, state);
        _needsUI.UpdateThirst(thirst, maxThirst);
        _needsUI.UpdateHunger(hunger, maxHunger);

        CheckForDying();
    }

    private void CheckForDying()
    {
        if (maxThirst - thirst < GlobalValues.Instance.globalThirstDecay ||
            maxHunger - hunger < GlobalValues.Instance.globalHungerDecay)
            Die();
    }

    public void Die()
    {
        CreateCorpse();
        Destroy(gameObject);
    }

    private void CreateCorpse()
    {
        CorpseSpawner.Instance.CreateCorpse(this);
    }

    public void StartRunning()
    {
        if (isRunning) return;
        state = CreatureState.Sprinting;
        isRunning = true;
        Speed = GetStats.GetStat(StatName.SprintSpeed);
    }

    public void StopRunning()
    {
        if (!isRunning) return;
        state = CreatureState.Running;
        isRunning = false;
        Speed = GetStats.GetStat(StatName.BaseSpeed);
    }

    public float ReturnCorpseSize()
    {
        return GetStats.GetStat(StatName.Weight);
    }
}