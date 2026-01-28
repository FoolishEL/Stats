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
                CalculateResultValue(valueProviderCached.Value);
            }
        }
        
        private IStatDecorator<T> valueProviderCached;

        protected SingleStatDecorator(IStatDecorator<T> decorator, bool needCyclicDispose) : base(needCyclicDispose)
        {
            valueProviderCached = decorator;
            AddDecoratorToDisposable(decorator);
        }

        protected void CalculateResultValue(T wrappedValue)
        {
            Value = CalculateResultValueInternal(wrappedValue);
        }

        /// <summary>
        /// Internal calculation for mutating original value
        /// </summary>
        protected abstract T CalculateResultValueInternal(T wrappedValue);

        protected override void DisposeRaw()
        {
            if (valueProviderCached is not null)
            {
                valueProviderCached.OnValueChanged -= CalculateResultValue;
            }
        }
    }
}