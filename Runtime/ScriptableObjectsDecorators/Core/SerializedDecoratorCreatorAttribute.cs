using System;

namespace Foolish.Stats.ScriptableObjectDecorators
{
    #if FOOLISH_UTILS
    using Utils;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SerializedDecoratorCreatorAttribute : InspectorSelector
    {
        public SerializedDecoratorCreatorAttribute(Type abstractType) : base(typeof(IStatDecoratorCreateOptions<>)
            .MakeGenericType(abstractType))
        {
        }
    }
    #else
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SerializedDecoratorCreatorAttribute : UnityEngine.PropertyAttribute
    {
        public Type PropertyType { get; private set; }
        public SerializedDecoratorCreatorAttribute(Type type) => PropertyType = type;
    }
    #endif
}