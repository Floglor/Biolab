using System;
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
        private Action _deathAction;

        [ShowInInspector] [ReadOnly] private float _timeSinceLastDecay;
        [ShowInInspector] [ReadOnly] private int _hungerDC;
        private float _decayInterval;

        [ShowInInspector] [ReadOnly] private float _hungerDebug;

        private void Start()
        {
            _hungerDC = (int) _statContainer.GetStat(StatName.HungerDC);
            _decayInterval = _statContainer.GetStat(StatName.HungerDecayInterval);
        }

        private void Update()
        {
            HungerDecay();
            _hungerDebug = _statContainer.GetStat(StatName.Hunger);
        }

        public void HungerDecay()
        {
           
            float hunger = _statContainer.GetStat(StatName.Hunger);
            float thirst = _statContainer.GetStat(StatName.Thirst);

            if (hunger <= MAX_HUNGER)
            {
                _statContainer.AddToStat(StatName.Hunger,
                    _statContainer.GetStat(StatName.HungerDecay) * (Time.deltaTime));
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
                _hungerDC = Math.Max(1, _hungerDC - 1);
                _timeSinceLastDecay = 0f;
            }
        }

        public void PerformDeathCheck()
        {
            if (_statContainer.GetStat(StatName.Hunger) >= MAX_HUNGER)
            {
                int roll = Roll3d6();

                if (roll > _hungerDC)
                {
                    _deathAction?.Invoke();
                }
            }
        }

        public void SatisfyHunger(float calories)
        {
            _statContainer.AddToStat(StatName.Hunger,
                -(calories * _statContainer.GetStat(StatName.CaloriesToHungerConversionRate)));
        }

        public float GetHunger()
        {
            return _statContainer.GetStat(StatName.Hunger);
        }

        public void SetDeathAction(Action deathAction)
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