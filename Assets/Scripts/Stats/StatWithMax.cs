using Stats.Genetics;

namespace Stats
{
    public class StatWithMax : BaseStat
    {
        public float MaxValue { get; private set; }
        public float CurrentValue { get; private set; }

        public StatWithMax(float baseValue, float maxValue) : base(baseValue)
        {
            MaxValue = maxValue;
            CurrentValue = maxValue;  // Initialize current value to max value
        }

        public override float Value => CurrentValue;

        public override void UpdateBaseStat(float value)
        {
            MaxValue += value;
            CurrentValue += value; // Update current value by the same amount, if necessary
            ClampCurrentValue();
        }

        public void ModifyCurrentValue(float delta)
        {
            CurrentValue += delta;
            ClampCurrentValue();
        }

        private void ClampCurrentValue()
        {
            if (CurrentValue > MaxValue) CurrentValue = MaxValue;
            if (CurrentValue < 0) CurrentValue = 0;
        }
    }
}