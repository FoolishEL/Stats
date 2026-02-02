namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="MultipleStatDecorator{T}"/> for int addtive
    /// </summary>
    public class IntMultipleSumStatsDecorator : MultipleStatDecorator<int>
    {
        public IntMultipleSumStatsDecorator(bool needCyclicDispose) : base(needCyclicDispose) { }
        protected override void RefreshStats()
        {
            var sum = 0;
            foreach (var valueProvider in ValueProviders)
            {
#if R3
                sum += valueProvider.ReactiveValue.CurrentValue;
            }
            valueCached.Value = sum;
#else
                sum += valueProvider.Value;
            }
            Value = sum;
#endif
        }
    }
}
