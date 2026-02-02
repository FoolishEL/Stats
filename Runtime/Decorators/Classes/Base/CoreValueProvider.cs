#if R3
using R3;
#else
using System;
#endif

namespace Foolish.Stats
{
    /// <summary>
    /// Basic decorator with providing value.
    /// </summary>
    /// <remarks>the value is mutable</remarks>
    public sealed class CoreValueProvider<T> : IStatDecorator<T>
    {
#if R3
        public ReadOnlyReactiveProperty<T> ReactiveValue => valueCached;
        public T Value => valueCached.Value;

        ReactiveProperty<T> valueCached;
#else
        /// <summary>
        /// value of decorator
        /// </summary>
        public T Value
        {
            get => currentValueCached;
            set
            {
                currentValueCached = value;
                OnValueChanged(currentValueCached);
            }
        }
        T currentValueCached;
        
        public event Action<T> OnValueChanged = delegate { };
#endif
        
        /// <summary>
        /// create new CoreValueProvider with given value
        /// </summary>
        public CoreValueProvider(T initialValue)
        {
#if R3
            valueCached = new(initialValue);
#else
            Value = initialValue;
#endif
        }
    }
}