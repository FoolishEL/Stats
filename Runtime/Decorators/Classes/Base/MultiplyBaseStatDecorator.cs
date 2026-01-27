using UnityEngine;

namespace Foolish.Stats
{
    /// <summary>
    /// Base class for multiply T value on float or int
    /// </summary>
    public abstract class MultiplyBaseStatDecorator<T> : SingleStatDecorator<T>
    {
        protected bool UsedIntAsMultiply;
        protected int MultiplyValueIntCached;
        protected float MultiplyValueFloatCached;

        public int MultiplyValueInt
        {
            get
            {
                if (!UsedIntAsMultiply)
                {
                    MultiplyValueIntCached = Mathf.RoundToInt(MultiplyValueFloatCached);
                }
                return MultiplyValueIntCached;
            }
            set
            {
                UsedIntAsMultiply = true;
                MultiplyValueIntCached = value;
                RefreshStats();
            }
        }

        public float MultiplyValueFloat
        {
            get
            {
                if (UsedIntAsMultiply)
                {
                    MultiplyValueFloatCached = MultiplyValueIntCached;
                }
                return MultiplyValueFloatCached;
            }
            set
            {
                UsedIntAsMultiply = false;
                MultiplyValueFloatCached = value;
                RefreshStats();
            }
        }

        protected void Init(IStatDecorator<T> decorator, int multiplyValueInt)
        {
            MultiplyValueIntCached = multiplyValueInt;
            ValueProvider = decorator;
            UsedIntAsMultiply = true;
            RefreshStats();
        }

        protected void Init(IStatDecorator<T> decorator, float multiplyValueFloat)
        {
            MultiplyValueFloatCached = multiplyValueFloat;
            UsedIntAsMultiply = false;
            ValueProvider = decorator;
            RefreshStats();
        }

        public enum RoundOption
        {
            MathRound,
            Ceil,
            Floor
        }
    }
}