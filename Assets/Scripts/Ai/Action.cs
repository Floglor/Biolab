using UnityEngine;

namespace Ai
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}