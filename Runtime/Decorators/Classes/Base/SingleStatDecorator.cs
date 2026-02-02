#if R3
using System;
using R3;
#endif

namespace Foolish.Stats
{
    /// <summary>
    /// Abstract class for any decorators that has only one <see cref="IStatDecorator{T}"/> source
    /// </summary>
    public abstract class SingleStatDecorator<T> : AbstractStatDecorator<T>
    {
#if R3
        IDisposable disposable; 
#endif
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
#if R3
                    disposable?.Dispose();
#else
                    valueProviderCached.OnValueChanged -= CalculateResultValue;
#endif
                }

                valueProviderCached = value;
#if R3
                disposable = valueCached.Subscribe(CalculateResultValue);
#else
                valueProviderCached.OnValueChanged += CalculateResultValue;
                CalculateResultValue(valueProviderCached.Value);
#endif
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
#if R3
            valueCached.Value =
#else
            Value =
#endif
            CalculateResultValueInternal(wrappedValue);
        }

        /// <summary>
        /// Internal calculation for mutating original value
        /// </summary>
        protected abstract T CalculateResultValueInternal(T wrappedValue);

        protected override void DisposeRaw()
        {
            if (valueProviderCached is not null)
            {
#if R3
                disposable?.Dispose();
#else
                valueProviderCached.OnValueChanged -= CalculateResultValue;
#endif
            }
        }
    }
}