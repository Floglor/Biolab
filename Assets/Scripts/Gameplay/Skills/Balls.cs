using UnityEngine;

namespace Gameplay.Skills
{
    public class Balls : Skill
    {
        public override bool TakeEffect(Vector3 clickCoordinates)
        {
            Debug.Log("Balls");
            return true;
        }
    }
}