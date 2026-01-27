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
                RefreshStats();
            }
        }

        /// <summary>
        /// creates new IntAddValueStatDecorator
        /// </summary>
        /// <param name="decorator">wrapped decorator</param>
        /// <param name="additiveValue">value to add to decorator's value</param>
        public IntAddValueStatDecorator(IStatDecorator<int> decorator, int additiveValue)
        {
            ValueProvider = decorator;
            AdditiveValue = additiveValue;
            RefreshStats();
        }

        protected override int CalculateResultValueInternal(int wrappedValue)
        {
            return wrappedValue + AdditiveValue;
        }
    }
}