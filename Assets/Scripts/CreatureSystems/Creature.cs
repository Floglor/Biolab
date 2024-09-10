using System;
using Ai;
using Pathfinding;
using Pathfinding.RVO;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;
using Random = UnityEngine.Random;


namespace CreatureSystems
{
    public class Creature : MonoBehaviour
    {
        private const int HiddenAlpha = 50;
        private const int NotHiddenAlpha = 255;

        public const float EatingRange = 3f;
        public const float ChompSize = 2f;
        public string speciesID;
        public CreatureType creatureType;

        public float maxThirst;

        public float ReproductionNeed => GetStats.GetStat(StatName.ReproductionNeed);
        public float reproductionNeed;

        public float startEyesight;
        public float eyesight;

        public float startEatingSpeed;
        [HideInInspector] public float eatingSpeed;

        public bool isMale;
        public bool isHidden;
        public bool isHerbivore;
        public bool isRunning;
        public bool isAlert;

        public AIPath aiPath;
        public Seeker seeker;
        public Vector3 lastFoodPosition;
        public Vector3 lastWaterPosition;

        public Action OnDrinkingFinished;

        public StateController stateController;
        public bool alreadyEating;
        public bool alreadyDrinking;

        [HideInInspector] public RVOController rvoController;
        public Corpse lastCorpse;

        public CreatureState state;

        private float _speed;

        public CustomTile LastFoodTile;

        // ReSharper disable once UnusedMember.Global
        public IMateSeeker AISeeker;


        public IEatingBehaviour EatingBehaviour;
        private INeedsDecay _needsDecayBehaviour;
        private IPredatorAwareness _predatorAwareness;
        private INeedsUI _needsUI;

        public IMovingBehaviour MovingBehaviour;
        public IRepeatMove RepeatMoveBehaviour;

        public IHungerSystem HungerSystem;
        public IWeightSystem WeightSystem;

        public Action<DeathReason> DeathAction;

        public Creature(IHungerSystem hungerSystem)
        {
            HungerSystem = hungerSystem;
        }

        public float Speed
        {
            private set
            {
                aiPath.maxSpeed = value;
                _speed = value;
            }
            get => _speed;
        }

        public float Hunger => HungerSystem.GetHunger();
        public float Thirst => HungerSystem.GetThirst();

        private void Update()
        {
            reproductionNeed = ReproductionNeed;
        }


        public GOStatContainer GetStats { get; private set; }

        public void ResetReproductionNeed()
        {
            GetStats.AddToStat(StatName.ReproductionNeed, -GetStats.GetStat(StatName.ReproductionNeed));
        }
        public void GetRandomName()
        {
            string[] maleNames = 
            {
                "Balldwin", "Ballrick", "Ballian", "Ballfred", "Ballson",
                "Ballard", "Ballton", "Ballford", "Ballster", "Ballmir",
                "Ballbert", "Ballwin", "Ballander", "Ballman", "Ballrickson",
                "Ballion", "Balltonius", "Balliam", "Ballther", "Ballmire",
                "Ballver", "Ballthor", "Ballius", "Ballgrim", "Ballrad",
                "Ballock", "Ballfrid", "Ballhorn", "Ballgar", "Ballionis"
            };

            string[] femaleNames = 
            {
                "Balleena", "Ballina", "Ballis", "Ballissa", "Ballora",
                "Ballette", "Balloraine", "Ballia", "Ballerina", "Ballinda",
                "Ballora", "Ballisandra", "Ballinae", "Balliana", "Ballith",
                "Ballerica", "Balliandra", "Ballanette", "Balluna", "Ballanda",
                "Ballinda", "Balliara", "Ballanora", "Ballalina", "Ballessa",
                "Ballastra", "Ballira", "Balletina", "Ballithra", "Balleisha"
            };

            
            
            string[] selectedNames = isMale ? maleNames : femaleNames;

            string creatureName = selectedNames[Random.Range(0, selectedNames.Length)];

            gameObject.name = creatureName;
        }
        protected void Start()
        {
            GetRandomName();
            if (isMale) this.name = $"{this.name} (Male)";
            DeathAction += Die;

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
            HungerSystem = GetComponent<IHungerSystem>();
            WeightSystem = GetComponent<IWeightSystem>();

            foreach (IReceiveDeathAction receiver in GetComponents<IReceiveDeathAction>())
            {
                receiver.SetDeathAction(DeathAction);
            }

            foreach (IReceiveStatContainer receiver in GetComponents<IReceiveStatContainer>())
            {
                receiver.SetStatContainer(GetStats);
            }


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
            CreatureList.Instance.RemoveCreature(this);
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

        private void Awake()
        {
            CreatureList.Instance.RegisterCreature(this);
        }

        private void InitializeStartingStats()
        {
            Speed = GetStats.GetStat(StatName.BaseSpeed);
            eyesight = startEyesight;

            aiPath.maxSpeed = Speed;
            eatingSpeed = startEatingSpeed;
            alreadyEating = false;
            maxThirst = GlobalValues.Instance.maxThirstDeathThreshold;
        }


        protected void NeedsDecay()
        {
            _needsDecayBehaviour.NeedsDecayTick(GetStats, state);
            _needsUI.UpdateThirst(Thirst, maxThirst);
        }
        
        public void Die(DeathReason deathReason = DeathReason.Cringe)
        {
            CreateCorpse();
            Destroy(gameObject);

            Debug.Log($"Creature {name} died of {Enum.GetName(typeof(DeathReason), deathReason)}");
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
            return GetStats.GetStat(StatName.Calories)/2;
        }
    }
}