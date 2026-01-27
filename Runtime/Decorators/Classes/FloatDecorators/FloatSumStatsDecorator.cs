namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="MultipleStatDecorator{T}"/> for float addtive
    /// </summary>
    public class FloatSumStatsDecorator : MultipleStatDecorator<float>
    {
        protected override void RefreshStats()
        {
            float sum = 0f;
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
