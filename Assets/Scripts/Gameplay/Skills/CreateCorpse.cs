using UnityEngine;

namespace Gameplay.Skills
{
    public class CreateCorpse : Skill
    {
        [SerializeField] private float _size;
        public override bool TakeEffect(Vector3 clickCoordinates)
        {
            CorpseSpawner.Instance.CreateCorpse(_size, clickCoordinates);
            return true;
        }
    }
}