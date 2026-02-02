using System;
using System.Collections.Generic;
#if R3
using R3;
#endif

namespace Foolish.Stats
{
    /// <summary>
    /// Base class for stat Decorator
    /// </summary>
    public abstract class AbstractStatDecorator<T> : IStatDecorator<T>, ICyclicDisposable
    {
        
#if R3
        public ReadOnlyReactiveProperty<T> ReactiveValue => valueCached;
        public T Value => valueCached.Value;
        protected ReactiveProperty<T> valueCached;
#else
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
#endif
        HashSet<ICyclicDisposable> disposables;
        bool ICyclicDisposable.IsDisposed => isDisposed;
        bool isDisposed;
        bool ICyclicDisposable.NeedCyclicDispose => needCyclicDispose;
        bool needCyclicDispose;

        protected AbstractStatDecorator(bool needCyclicDispose)
        {
            disposables = new();
            this.needCyclicDispose = needCyclicDispose;
            isDisposed = false;

        }

        protected abstract void DisposeRaw();

        protected void AddDecoratorToDisposable<TV>(IStatDecorator<TV> decorator)
        {
            if (decorator is ICyclicDisposable cyclicDisposable)
                disposables.Add(cyclicDisposable);
        }

        protected void RemoveDecoratorFromDisposable<TV>(IStatDecorator<TV> decorator)
        {
            if (decorator is ICyclicDisposable cyclicDisposable)
                disposables.Remove(cyclicDisposable);
        }

        protected void AddDecoratorToDisposable<TV>(params IStatDecorator<TV>[] decorators)
        {
            if (decorators is null)
                return;
            foreach (var statDecorator in decorators)
                AddDecoratorToDisposable(statDecorator);
        }
        
        void IDisposable.Dispose()
        {
            if(isDisposed)
                return;
            isDisposed = true;
#if R3
            valueCached?.Dispose();
#endif
            DisposeRaw();
            foreach (var disposable in disposables)
            {
                if(disposable.NeedCyclicDispose && !disposable.IsDisposed)
                    disposable.Dispose();   
            }
            disposables.Clear();
        }
    }
}