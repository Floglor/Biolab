using System;
using CreatureSystems;
using UnityEngine;

namespace Gameplay.Skills
{
    public class Kill : Skill
    {
        public float radius = 0.5f;  

        public override bool TakeEffect(Vector3 clickCoordinates)
        {
            Debug.Log($"Killing creatures within radius at ({clickCoordinates.x}, {clickCoordinates.y})");

            Collider2D[] colliders = Physics2D.OverlapCircleAll(clickCoordinates, radius);
            bool creatureKilled = false;

            foreach (Collider2D overlapCollider in colliders)
            {
                Creature creature = overlapCollider.GetComponent<Creature>();
                
                if (creature == null) continue;
                
                creature.Die();  
                creatureKilled = true;
            }

            return creatureKilled;
        }
        
    }
}