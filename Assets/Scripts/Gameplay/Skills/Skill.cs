using System;
using UnityEngine;

namespace Gameplay.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        public int Cost;
        public string Name;
        public abstract bool TakeEffect(Vector3 clickCoordinates);
        public GameObject EffectGO;

        private void Start()
        {
            if (EffectGO == null) return;
            EffectGO = Instantiate(EffectGO);
            EffectGO.SetActive(false);
        }
    }
}