using System;

namespace Foolish.Stats
{
    /// <summary>
    /// Basic decorator with providing value.
    /// </summary>
    /// <remarks>the value is mutable</remarks>
    public sealed class CoreValueProvider<T> : IStatDecorator<T>
    {
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

        /// <summary>
        /// create new CoreValueProvider with given value
        /// </summary>
        public CoreValueProvider(T initialValue) => Value = initialValue;
    }
}