using System;

namespace Foolish.Stats
{
    /// <summary>
    /// basic interface for any decoratable stat
    /// </summary>
    public interface IStatDecorator<T> : IDisposable
    {
        /// <summary>
        /// Get current value of decorator
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// event on change resulting value
        /// </summary>
        event Action<T> OnValueChanged;
    }
}