using System;
#if R3
using R3;
#endif

namespace Foolish.Stats
{
    /// <summary>
    /// base decorator for cross-types operations
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class CrossTypeOperationStatDecorator<T1, T2> : AbstractStatDecorator<T1>
    {
        IStatDecorator<T1> decorator1;
        IStatDecorator<T2> decorator2;
#if R3
        IDisposable disposable;
#endif

        protected CrossTypeOperationStatDecorator(IStatDecorator<T1> decorator1, IStatDecorator<T2> decorator2, bool needCyclicDispose) : base(needCyclicDispose)
        {
            this.decorator1 =  decorator1;
            this.decorator2 = decorator2;
            
#if R3
            var d1 = decorator1.ReactiveValue.Subscribe(Changed1);
            var d2 = decorator2.ReactiveValue.Subscribe(Changed2);
            disposable = Disposable.Combine(d1, d2);
#else
            decorator1.OnValueChanged += Changed1;
            decorator2.OnValueChanged += Changed2;
#endif
            AddDecoratorToDisposable(decorator1);
            AddDecoratorToDisposable(decorator2);
        }

        void Changed1(T1 value)
        {
#if R3
            valueCached.Value = CalculateOperationResult(value, decorator2.ReactiveValue.CurrentValue);
#else
            Value = CalculateOperationResult(value, decorator2.Value);
#endif
        }

        void Changed2(T2 value)
        {
#if R3
            valueCached.Value = CalculateOperationResult(decorator1.ReactiveValue.CurrentValue, value);
#else
            Value = CalculateOperationResult(decorator1.Value, value);
#endif
        }

        protected abstract T1 CalculateOperationResult(T1 value1, T2 value2);

        protected override void DisposeRaw()
        {
#if R3
            disposable.Dispose();
#else
            decorator1.OnValueChanged -= Changed1;
            decorator2.OnValueChanged -= Changed2;
#endif
        }
    }
}