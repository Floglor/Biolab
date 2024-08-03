namespace Stats.Genetics
{
    public abstract class BaseStat
    {
        public float BaseValue { get; protected set; }
        public abstract float Value { get; }

        protected BaseStat(float baseValue)
        {
            BaseValue = baseValue;
        }
        
        public abstract void UpdateBaseStat(float value);
    }
}