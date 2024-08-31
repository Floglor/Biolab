using System;
using Stats.Genetics;
using UnityEngine;

namespace Gameplay.Skills
{
    public class TestSkill : Skill
    {
        public override void TakeEffect(Vector3 clickCoordinates)
        {
            Debug.Log($"Test Skill fired at ({clickCoordinates.x}, {clickCoordinates.y}");
        }
    }
}