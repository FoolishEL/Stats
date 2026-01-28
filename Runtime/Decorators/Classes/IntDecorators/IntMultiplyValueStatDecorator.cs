using UnityEngine;

namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="CrossTypeValueOperationStatDecorator{T,T2,TV2}"/> for int on int multiply
    /// </summary>
    public sealed class IntMultiplyIntValueStatDecorator : CrossTypeValueOperationStatDecorator<int, int, IntValueProvider>
    {

        public IntMultiplyIntValueStatDecorator(IStatDecorator<int> decorator, int initialValueProvider, bool needCyclicDispose) : base(decorator, initialValueProvider, needCyclicDispose)
        {
        }
        protected override int CalculateResultValueInternal(int wrappedValue) => wrappedValue * operationValueProvider.Value;
        protected override void OnOperationValueChanged(int value) => Value = value * operationValueProvider.Value;
    }

    /// <summary>
    /// Default realisation for <see cref="CrossTypeValueOperationStatDecorator{T,T2,TV2}"/> for int on int multiply
    /// </summary>
    public sealed class IntMultiplyFloatValueStatDecorator : CrossTypeValueOperationStatDecorator<int, float, FloatValueProvider>
    {

        RoundOption roundOption;

        public IntMultiplyFloatValueStatDecorator(IStatDecorator<int> decorator, float initialValueProvider, bool needCyclicDispose, RoundOption roundOption = RoundOption.MathRound) : base(decorator, initialValueProvider, needCyclicDispose)
        {
            this.roundOption = roundOption;
        }
        protected override int CalculateResultValueInternal(int wrappedValue) => GetResultValue(operationValueProvider.Value * wrappedValue);
        protected override void OnOperationValueChanged(float value) => Value = GetResultValue(operationValueProvider.Value * value);

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