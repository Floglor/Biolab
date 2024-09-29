using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ToDelete
{
    public class GurpsDamage : MonoBehaviour
    {
        public int FlexibleArmor;
        public int DamageVEbalo;

        [Button]
        public void TakeDamage()
        {
            int damage = Math.Abs(DamageVEbalo - FlexibleArmor) + Math.Abs(DamageVEbalo - FlexibleArmor) / 3;

            Debug.Log($"You took {damage} damage {DamageVEbalo} - {FlexibleArmor} + ({DamageVEbalo} - {FlexibleArmor}) / 3");
        }
    }
}