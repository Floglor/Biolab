using System;

namespace Stats
{
    [Serializable]
    public class Buff
    {
        public StatName statName;
        public float StatValue;
        public bool IsPermanent;
        public bool IsPercentage;
        public float Time;
    }
}