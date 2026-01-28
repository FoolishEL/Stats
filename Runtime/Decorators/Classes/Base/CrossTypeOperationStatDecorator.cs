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

        protected CrossTypeOperationStatDecorator(IStatDecorator<T1> decorator1, IStatDecorator<T2> decorator2, bool needCyclicDispose) : base(needCyclicDispose)
        {
            this.decorator1 =  decorator1;
            this.decorator2 = decorator2;
            
            decorator1.OnValueChanged += Changed1;
            decorator2.OnValueChanged += Changed2;
            AddDecoratorToDisposable(decorator1);
            AddDecoratorToDisposable(decorator2);
        }

        void Changed1(T1 value) => Value = CalculateOperationResult(value, decorator2.Value);

        void Changed2(T2 value) => Value = CalculateOperationResult(decorator1.Value, value);
        
        protected abstract T1 CalculateOperationResult(T1 value1, T2 value2);

        protected override void DisposeRaw()
        {
            decorator1.OnValueChanged -= Changed1;
            decorator2.OnValueChanged -= Changed2;
        }
    }
}