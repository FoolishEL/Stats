using System.Collections.Generic;
#if R3
using System;
using R3;
#endif

namespace Foolish.Stats
{
    /// <summary>
    /// Abstract class for any decorator that has multiple <see cref="IStatDecorator{T}"/> sources
    /// </summary>
    public abstract class MultipleStatDecorator<T> : AbstractStatDecorator<T>
    {
#if R3
        Dictionary<IStatDecorator<T>, IDisposable> disposables;
#else
#endif
        /// <summary>
        /// All value providers
        /// </summary>
        protected HashSet<IStatDecorator<T>> ValueProviders = new HashSet<IStatDecorator<T>>();

        protected MultipleStatDecorator(bool needCyclicDispose) : base(needCyclicDispose)
        {
#if R3
            disposables = new();
#endif
        }
        /// <summary>
        /// Add new <see cref="IStatDecorator{T}"/> to be decorated
        /// </summary>
        public bool AddStatDecorator(IStatDecorator<T> decorator)
        {
            if (ValueProviders.Add(decorator))
            {
#if R3
                disposables.Add(decorator,decorator.ReactiveValue.Subscribe(RefreshAllStats));
#else
                decorator.OnValueChanged += RefreshAllStats;
#endif
                AddDecoratorToDisposable(decorator);
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
#if R3
                if (disposables.Remove(decorator, out var disposable))
                {
                    disposable?.Dispose();
                }
#else
                decorator.OnValueChanged -= RefreshAllStats;
#endif
                RemoveDecoratorFromDisposable(decorator);
                RefreshStats();
                return true;
            }
            return false;
        }

        protected void RefreshAllStats(T value) => RefreshStats();

        protected abstract void RefreshStats();

        protected override void DisposeRaw()
        {
#if R3
            foreach (var disposable in disposables.Values)
            {
                disposable.Dispose();
            }
            disposables.Clear();
            
#else
            foreach (var valueProvider in ValueProviders)
            {
                valueProvider.OnValueChanged -= RefreshAllStats;
            }
#endif
        }
    }
}