using System.Collections.Generic;

namespace Foolish.Stats
{
    /// <summary>
    /// Abstract class for any decorator that has multiple <see cref="IStatDecorator{T}"/> sources
    /// </summary>
    public abstract class MultipleStatDecorator<T> : AbstractStatDecorator<T>
    {
        /// <summary>
        /// All value providers
        /// </summary>
        protected HashSet<IStatDecorator<T>> ValueProviders = new HashSet<IStatDecorator<T>>();

        /// <summary>
        /// Add new <see cref="IStatDecorator{T}"/> to be decorated
        /// </summary>
        public bool AddStatDecorator(IStatDecorator<T> decorator)
        {
            if (ValueProviders.Add(decorator))
            {
                decorator.OnValueChanged += RefreshAllStats;
                RefreshStats();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove <see cref="IStatDecorator{T}"/> from decoration sources
        /// </summary>
        public bool RemoveStatDecorator(IStatDecorator<T> decorator)
        {
            if (ValueProviders.Remove(decorator))
            {
                decorator.OnValueChanged -= RefreshAllStats;
                RefreshStats();
                return true;
            }
            return false;
        }
        
        protected void RefreshAllStats(T value)
        {
            RefreshStats();
        }
    }
}