using UnityEngine;

namespace Ai
{
    public abstract class OneTimeAction : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}