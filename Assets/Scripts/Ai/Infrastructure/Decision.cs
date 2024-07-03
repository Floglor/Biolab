using UnityEngine;

namespace Ai.Infrastructure
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}