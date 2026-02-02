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
    /// <remarks>The value is immutable.
    /// Also consider that as value is immutable it will cache it.
    /// So if you are using reference value as T make sure it is protected from unwanted changes
    /// </remarks>
    public sealed class RawValueProvider<T> : IStatDecorator<T>
    {
#if R3
        public ReadOnlyReactiveProperty<T> ReactiveValue { get; }
        public T Value => ReactiveValue.CurrentValue;
#else
        
        public T Value => initialValue;
        readonly T initialValue;
        
        public event Action<T> OnValueChanged = delegate { };
#endif

        /// <summary>
        /// Static constructor fot <see cref="RawValueProvider{T}"/>
        /// it will get ot create RawValueProvider for value.
        /// </summary>
        public static RawValueProvider<T> FromValue(T value)
        {
            if (cachedInstances.TryGetValue(value, out var fromValue))
            {
                return fromValue;
            }

            var newInstance = new RawValueProvider<T>(value);
            cachedInstances.Add(value, newInstance);
            return newInstance;
        }

        static System.Collections.Generic.Dictionary<T, RawValueProvider<T>> cachedInstances = new();

        RawValueProvider(T value)
        {
#if R3
            ReactiveValue = Observable.Return(value).ToReadOnlyReactiveProperty();
#else
            initialValue = value;
#endif
        }
    }
}