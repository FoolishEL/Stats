using System;

namespace Foolish.Stats
{
    /// <summary>
    /// Base class for stat Decorator
    /// </summary>
    public abstract class AbstractStatDecorator<T> : IStatDecorator<T>
    {
        public T Value
        {
            get => valueCached;
            protected set
            {
                valueCached = value;
                OnValueChanged.Invoke(value);
            }
        }
        private T valueCached;
        
        public event Action<T> OnValueChanged = delegate {};

        /// <summary>
        /// Recalculate resulting value
        /// </summary>
        protected abstract void RefreshStats();

        public abstract void Dispose();
    }

}