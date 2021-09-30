using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/AlwaysTrueDecision")]
    public class AlwaysTrueDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return true;
        }
    }
}