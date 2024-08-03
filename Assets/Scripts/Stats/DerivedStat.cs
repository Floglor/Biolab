using System;

namespace Stats.Genetics
{
    public class DerivedStat : BaseStat
    {
        private readonly Func<float> _calculationFunction;

        public override float Value => _calculationFunction();

        public DerivedStat(Func<float> calculationFunction) : base(0)
        {
            _calculationFunction = calculationFunction;
        }

        public override void UpdateBaseStat(float value)
        {
            BaseValue += _calculationFunction() + value;
        }
    }
}