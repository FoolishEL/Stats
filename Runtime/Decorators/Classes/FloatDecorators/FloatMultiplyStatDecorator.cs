namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="CrossTypeValueOperationStatDecorator{T,T2,TV2}"/> for float on float multiply
    /// </summary>
    public sealed class FloatMultiplyFloatValueStatDecorator : CrossTypeValueOperationStatDecorator<float, float, FloatValueProvider>
    {
        public FloatMultiplyFloatValueStatDecorator(IStatDecorator<float> decorator, float initialValueProvider, bool needCyclicDispose) : base(decorator, initialValueProvider, needCyclicDispose)
        {
        }
        protected override float CalculateResultValueInternal(float wrappedValue) => wrappedValue * operationValueProvider.Value;
        protected override void OnOperationValueChanged(float value)
        {
#if R3
            valueCached.Value = ValueProvider.ReactiveValue.CurrentValue * operationValueProvider.Value;
#else
            Value = ValueProvider.Value * operationValueProvider.Value;
#endif
        }
    }

    /// <summary>
    /// Default realisation for <see cref="CrossTypeValueOperationStatDecorator{T,T2,TV2}"/> for float on int multiply
    /// </summary>
    public sealed class FloatMultiplyIntValueStatDecorator : CrossTypeValueOperationStatDecorator<float, int, IntValueProvider>
    {
        public FloatMultiplyIntValueStatDecorator(IStatDecorator<float> decorator, int initialValueProvider, bool needCyclicDispose) : base(decorator, initialValueProvider, needCyclicDispose)
        {
        }
        protected override float CalculateResultValueInternal(float wrappedValue) => wrappedValue * operationValueProvider.Value;
        protected override void OnOperationValueChanged(int value)
        {
#if R3
            valueCached.Value = value * operationValueProvider.Value;
#else
            Value = value * operationValueProvider.Value;
#endif
        }
    }
}