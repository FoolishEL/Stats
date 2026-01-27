namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="MultiplyBaseStatDecorator{T}"/> for float on int or float multiply
    /// </summary>
    public sealed class FloatMultiplyStatDecorator : MultiplyBaseStatDecorator<float>
    {
        public FloatMultiplyStatDecorator(IStatDecorator<float> decorator, int multiplyValueInt)
        {
            Init(decorator, multiplyValueInt);
        }
        
        public FloatMultiplyStatDecorator(IStatDecorator<float> decorator, float multiplyValueFloat)
        {
            Init(decorator, multiplyValueFloat);
        }

        protected override float CalculateResultValueInternal(float wrappedValue)
        {
            return wrappedValue * (UsedIntAsMultiply ? MultiplyValueIntCached : MultiplyValueFloatCached);
        }
    }
}