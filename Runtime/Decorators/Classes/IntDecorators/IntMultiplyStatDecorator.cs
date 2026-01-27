using UnityEngine;

namespace Foolish.Stats
{
    /// <summary>
    /// Default realisation for <see cref="MultiplyBaseStatDecorator{T}"/> for int on int or float multiply
    /// </summary>
    public sealed class IntMultiplyStatDecorator : MultiplyBaseStatDecorator<int>
    {
        public RoundOption CurrentRoundOption { get; set; }

        public IntMultiplyStatDecorator(IStatDecorator<int> decorator, int multiplyValueInt)
        {
            CurrentRoundOption = RoundOption.MathRound;
            Init(decorator, multiplyValueInt);
        }

        public IntMultiplyStatDecorator(IStatDecorator<int> decorator, float multiplyValueFloat,
            RoundOption roundOption = RoundOption.MathRound)
        {
            CurrentRoundOption = roundOption;
            Init(decorator, multiplyValueFloat);
        }

        protected override int CalculateResultValueInternal(int wrappedValue)
        {
            return UsedIntAsMultiply ? wrappedValue * MultiplyValueInt : GetResultValue(wrappedValue);
        }

        int GetResultValue(int wrappedValue) => CurrentRoundOption switch
        {
            RoundOption.Ceil => Mathf.CeilToInt(wrappedValue * MultiplyValueFloat),
            RoundOption.Floor => Mathf.FloorToInt(wrappedValue * MultiplyValueFloat),
            _ => Mathf.RoundToInt(wrappedValue * MultiplyValueFloat),
        };
    }
}