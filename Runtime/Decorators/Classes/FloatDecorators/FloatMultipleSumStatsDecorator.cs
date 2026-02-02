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
#if R3
                sum += valueProvider.ReactiveValue.CurrentValue;
#else
                sum += valueProvider.Value;
#endif
            }
#if R3
            valueCached.Value = sum;
#else
            Value = sum;
#endif
        }
    }
}
