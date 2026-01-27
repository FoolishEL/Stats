namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="MultipleStatDecorator{T}"/> for int addtive
    /// </summary>
    public class IntSumStatsDecorator : MultipleStatDecorator<int>
    {
        protected override void RefreshStats()
        {
            var sum = 0;
            foreach (var valueProvider in ValueProviders)
            {
                sum += valueProvider.Value;
            }

            Value = sum;
        }
        
        public override void Dispose()
        {
            foreach (var valueProvider in ValueProviders)
            {
                valueProvider.OnValueChanged -= RefreshAllStats;
                valueProvider.Dispose();
            }
        }
    }

}
