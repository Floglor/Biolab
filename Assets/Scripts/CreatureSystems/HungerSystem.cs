using System;
using Stats;
using UnityEngine;

namespace CreatureSystems
{
    [RequireComponent(typeof(Creature))]
    public class HungerSystem : MonoBehaviour, IHungerSystem, IReceiveDeathAction
    {
        private GOStatContainer _statContainer;
        private Action _deathAction;

        private float _timeSinceLastDecay;
        private int _hungerDC;  
        private float _decayInterval;

        private void Start()
        {
            _statContainer = GetComponent<Creature>().GetStats;

            _hungerDC = (int)_statContainer.GetStat(StatName.HungerDC);
            _decayInterval = _statContainer.GetStat(StatName.HungerDecayInterval);
        }

        private void Update()
        {
            HungerDecay();
        }

        private void HungerDecay()
        {
            float hunger = _statContainer.GetStat(StatName.Hunger);
            if (hunger >= 0)
            {
                _statContainer.AddToStat(StatName.Hunger, _statContainer.GetStat(StatName.HungerDecay) * Time.deltaTime);
            }

            _timeSinceLastDecay += Time.deltaTime;

            if (hunger < 0 && _timeSinceLastDecay >= _decayInterval)
            {
                _hungerDC = Math.Max(1, _hungerDC - 1);
                _timeSinceLastDecay = 0f;
            }
        }

        public void PerformDeathCheck()
        {
            if (_statContainer.GetStat(StatName.Hunger) < 0)
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
                calories * _statContainer.GetStat(StatName.CaloriesToHungerConversionRate));
        }

        public float GetHunger()
        {
            return _statContainer.GetStat(StatName.Hunger);
        }

        public void SetDeathAction(Action deathAction)
        {
            _deathAction = deathAction;
        }

        private static int Roll3d6()
        {
            return UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7);
        }
    }
}
