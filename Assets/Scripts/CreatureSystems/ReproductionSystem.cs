using Stats;
using UnityEngine;

namespace CreatureSystems
{
    public class ReproductionSystem : MonoBehaviour, IReproductionSystem, IReceiveStatContainer
    {
        private GOStatContainer _statContainer;
        public void SetStatContainer(GOStatContainer statContainer)
        {
            _statContainer = statContainer;
        }
    }

    public interface IReproductionSystem
    {
    }
}