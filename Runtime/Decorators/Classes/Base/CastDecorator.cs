using System;
#if R3
using R3;
#endif

namespace Foolish.Stats
{
    /// <summary>
    /// decorator that cast value from T1 into T2
    /// </summary>
    public abstract class CastDecorator<T1, T2> : AbstractStatDecorator<T2>
    {
        IStatDecorator<T1> sourceDecorator;
#if R3
        IDisposable subscription;
#endif
        protected CastDecorator(IStatDecorator<T1> sourceDecorator, bool needCyclicDispose) : base(needCyclicDispose)
        {
            this.sourceDecorator = sourceDecorator;
            AddDecoratorToDisposable(sourceDecorator);
#if R3
            valueCached = new(Convert(sourceDecorator.ReactiveValue.CurrentValue));
            subscription = sourceDecorator.ReactiveValue.Subscribe(OnSourceValueChanged);
#else
            sourceDecorator.OnValueChanged += OnSourceValueChanged;
#endif

        }
        void OnSourceValueChanged(T1 changedValue)
        {
#if R3
            valueCached.Value =
#else
            Value =
#endif
            Convert(changedValue);
        }

        protected abstract T2 Convert(T1 value);
        
        protected override void DisposeRaw()
        {
#if R3
            subscription?.Dispose();
#else
            sourceDecorator.OnValueChanged -= OnSourceValueChanged;
#endif

        }
    }
}