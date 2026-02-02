using UnityEngine;

namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="CrossTypeValueOperationStatDecorator{T,T2,TV2}"/> for int with a float sum
    /// </summary>
    public sealed class IntSumFloatValueStatDecorator : CrossTypeValueOperationStatDecorator<int, float, FloatValueProvider>
    {
        RoundOption roundOption;

        public IntSumFloatValueStatDecorator(IStatDecorator<int> decorator, float initialValueProvider, bool needCyclicDispose, RoundOption roundOption = RoundOption.MathRound) 
            : base(decorator, initialValueProvider, needCyclicDispose) => this.roundOption = roundOption;
        protected override int CalculateResultValueInternal(int wrappedValue) => GetResultValue(operationValueProvider.Value + wrappedValue);
        protected override void OnOperationValueChanged(float value)
        {
#if R3
            valueCached.Value = 
#else
            Value =
#endif
            GetResultValue(operationValueProvider.Value + value);
        }

        int GetResultValue(float value)
        {
            switch (roundOption)
            {
                case RoundOption.Ceil:
                    return Mathf.CeilToInt(value);
                case RoundOption.Floor:
                    return Mathf.FloorToInt(value);
                case RoundOption.MathRound:
                default:
                    return Mathf.RoundToInt(value);
            }
        }
    }
}