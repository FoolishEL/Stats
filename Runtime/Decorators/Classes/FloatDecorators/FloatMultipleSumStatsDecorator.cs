namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="MultipleStatDecorator{T}"/> for float addtive
    /// </summary>
    public class FloatMultipleSumStatsDecorator : MultipleStatDecorator<float>
    {
        public FloatMultipleSumStatsDecorator(bool needCyclicDispose) : base(needCyclicDispose) {}
        protected override void RefreshStats()
        {
            float sum = 0f;
            foreach (var valueProvider in ValueProviders)
            {
                sum += valueProvider.Value;
            }
    
            Value = sum;
        }

        protected override void DisposeRaw()
        {
            foreach (var valueProvider in ValueProviders)
            {
                valueProvider.OnValueChanged -= RefreshAllStats;
            }
        }
    }
}
