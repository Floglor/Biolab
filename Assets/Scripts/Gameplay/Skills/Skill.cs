using UnityEngine;

namespace Gameplay.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        public int Cost;
        public string Name;
        public abstract void TakeEffect(Vector3 clickCoordinates);
    }
}