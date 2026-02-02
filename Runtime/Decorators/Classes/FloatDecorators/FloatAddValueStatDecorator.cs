namespace Foolish.Stats
{
    /// <summary>
    /// Realisation of <see cref="SingleStatDecorator{T}"/> for float values additing
    /// </summary>
    public class FloatAddValueStatDecorator : SingleStatDecorator<float>
    {
        float additiveValueCached;

        /// <summary>
        /// Value that adds to decoratable value
        /// </summary>
        public float AdditiveValue
        {
            get => additiveValueCached;
            set
            {
                additiveValueCached = value;
#if R3
                CalculateResultValue(ValueProvider.ReactiveValue.CurrentValue);
#else
                CalculateResultValue(ValueProvider.Value);
#endif
            }
        }

        /// <summary>
        /// creates new FloatAddValueStatDecorator
        /// </summary>
        /// <param name="decorator">wrapped decorator</param>
        /// <param name="additiveValue">value to add to decorator's value</param>
        /// <param name="needCyclicDispose">need </param>
        public FloatAddValueStatDecorator(IStatDecorator<float> decorator, float additiveValue, bool needCyclicDispose) : base(decorator, needCyclicDispose) => AdditiveValue = additiveValue;

        protected override float CalculateResultValueInternal(float wrappedValue)
        {
            return wrappedValue + AdditiveValue;
        }
    }
}