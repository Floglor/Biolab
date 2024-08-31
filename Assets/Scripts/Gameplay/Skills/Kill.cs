using System;
using CreatureSystems;
using UnityEngine;

namespace Gameplay.Skills
{
    public class Kill : Skill
    {
        public override void TakeEffect(Vector3 clickCoordinates)
        {
            Debug.Log($"Killing creature at ({clickCoordinates.x}, {clickCoordinates.y}");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.GetComponent<Creature>() != null)
            {
                hit.collider.GetComponent<Creature>().Die();
            }
        }
    }
}