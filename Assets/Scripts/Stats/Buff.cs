using System;

namespace Stats
{
    [Serializable]
    public class Buff
    {
        public GeneStat geneStat;
        public float StatValue;
        public bool IsPermanent;
        public bool IsPercentage;
        public float Time;
    }
}