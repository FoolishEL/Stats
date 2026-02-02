#if R3
using R3;
#else
using System;
#endif

namespace Foolish.Stats
{
    /// <summary>
    /// basic interface for any decoratable stat
    /// </summary>
    public interface IStatDecorator<T>
    {
#if R3
        /// <summary>
        /// Reactive value of decorator
        /// </summary>
        public ReadOnlyReactiveProperty<T> ReactiveValue { get; }

        /// <summary>
        /// Get current value of decorator
        /// </summary>
        public T Value { get; }
#else
        /// <summary>
        /// Get current value of decorator
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// event on change resulting value
        /// </summary>
        event Action<T> OnValueChanged;
#endif
    }
}