namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="CrossTypeValueOperationStatDecorator{T,T2,TV2}"/> for float with an int sum
    /// </summary>
    public sealed class FloatSumIntValueStatDecorator : CrossTypeValueOperationStatDecorator<float, int, IntValueProvider>
    {
        public FloatSumIntValueStatDecorator(IStatDecorator<float> decorator, int initialValueProvider, bool needCyclicDispose) : base(decorator, initialValueProvider, needCyclicDispose)
        {
        }
        protected override float CalculateResultValueInternal(float wrappedValue) => wrappedValue + operationValueProvider.Value;
        protected override void OnOperationValueChanged(int value) => Value = value + operationValueProvider.Value;
    }
}