namespace Foolish.Stats
{
    /// <summary>
    /// Realisation of <see cref="SingleStatDecorator{T}"/> for int values additing
    /// </summary>
    public class IntAddValueStatDecorator : SingleStatDecorator<int>
    {
        int additiveValueCached;

        /// <summary>
        /// Value that adds to decoratable value
        /// </summary>
        public int AdditiveValue
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
        /// creates new IntAddValueStatDecorator
        /// </summary>
        /// <param name="decorator">wrapped decorator</param>
        /// <param name="additiveValue">value to add to decorator's value</param>
        /// <param name="needCyclicDispose">does this instance need to be automatically disposed when used by other decorators?</param>
        public IntAddValueStatDecorator(IStatDecorator<int> decorator, int additiveValue, bool needCyclicDispose) : base(decorator, needCyclicDispose)
        {
            AdditiveValue = additiveValue;
        }

        protected override int CalculateResultValueInternal(int wrappedValue)
        {
            return wrappedValue + AdditiveValue;
        }
    }
}