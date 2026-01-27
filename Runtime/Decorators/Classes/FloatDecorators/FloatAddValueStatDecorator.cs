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
                RefreshStats();
            }
        }

        /// <summary>
        /// creates new FloatAddValueStatDecorator
        /// </summary>
        /// <param name="decorator">wrapped decorator</param>
        /// <param name="additiveValue">value to add to decorator's value</param>
        public FloatAddValueStatDecorator(IStatDecorator<float> decorator, float additiveValue)
        {
            ValueProvider = decorator;
            AdditiveValue = additiveValue;
            RefreshStats();
        }

        protected override float CalculateResultValueInternal(float wrappedValue)
        {
            return wrappedValue + AdditiveValue;
        }
    }
}