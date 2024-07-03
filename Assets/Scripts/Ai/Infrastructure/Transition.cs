using System;

namespace Ai.Infrastructure
{
    [Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}