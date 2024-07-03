using UnityEngine;

namespace Ai.Actions
{
    public abstract class OneTimeAction : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}