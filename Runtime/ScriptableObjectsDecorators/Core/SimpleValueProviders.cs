using System;
using UnityEngine;

namespace Foolish.Stats.ScriptableObjectDecorators
{
    [Serializable]
    public class SimpleValueProvidersCreateOptions<T> : IStatDecoratorCreateOptions<T>
    {
        [field: SerializeField]
        public T Value { get; private set; }

        [SerializeField]
        CreationType creationType;

        public IStatDecorator<T> CreateDecorator()
        {
            switch (creationType)
            {
                case CreationType.CoreValueProvider:
                    return new CoreValueProvider<T>(Value);
                case CreationType.RawValueProvider:
                    return RawValueProvider<T>.FromValue(Value);
                default:
                    throw new ArgumentException("Invalid creation Type.");
            }
        }

        public enum CreationType
        {
            CoreValueProvider,
            RawValueProvider
        }
    }

    [Serializable]
    public class FloatSimpleValueProvidersCreateOptions : SimpleValueProvidersCreateOptions<float>
    {
        
    }
    
    [Serializable]
    public class IntSimpleValueProvidersCreateOptions : SimpleValueProvidersCreateOptions<int>
    {
        
    }
}