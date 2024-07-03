using UnityEngine;

namespace Ai.Infrastructure
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}