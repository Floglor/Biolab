using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Decisions
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