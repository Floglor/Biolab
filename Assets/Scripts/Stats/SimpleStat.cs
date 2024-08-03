namespace Stats.Genetics
{
    public class SimpleStat : BaseStat
    {
        public override float Value => BaseValue;
        
        public override void UpdateBaseStat(float value)
        {
            BaseValue += value;
        }

        public SimpleStat(float baseValue) : base(baseValue) {}
        
    }
}