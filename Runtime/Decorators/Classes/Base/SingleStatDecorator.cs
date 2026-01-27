namespace Foolish.Stats
{
    /// <summary>
    /// Abstract class for any decorators that has only one <see cref="IStatDecorator{T}"/> source
    /// </summary>
    public abstract class SingleStatDecorator<T> : AbstractStatDecorator<T>
    {
        /// <summary>
        /// Current value provider
        /// </summary>
        public IStatDecorator<T> ValueProvider
        {
            get => valueProviderCached;
            set
            {
                if (valueProviderCached is not null)
                {
                    valueProviderCached.OnValueChanged -= CalculateResultValue;
                }

                valueProviderCached = value;
                valueProviderCached.OnValueChanged += CalculateResultValue;
                RefreshStats();
            }
        }
        
        IStatDecorator<T> valueProviderCached;

        void CalculateResultValue(T wrappedValue)
        {
            Value = CalculateResultValueInternal(wrappedValue);
        }

        protected sealed override void RefreshStats()
        {
            CalculateResultValue(valueProviderCached.Value);
        }

        /// <summary>
        /// Internal calculation for mutating original value
        /// </summary>
        protected abstract T CalculateResultValueInternal(T wrappedValue);

        public override void Dispose()
        {
            if (valueProviderCached is not null)
            {
                valueProviderCached.OnValueChanged -= CalculateResultValue;
            }
        }
    }

}