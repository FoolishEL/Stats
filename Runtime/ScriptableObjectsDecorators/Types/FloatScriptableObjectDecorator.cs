using UnityEngine;

namespace Foolish.Stats.ScriptableObjectDecorators
{
    public class FloatScriptableObjectDecorator : ScriptableObjectDecorator<float>
    {
        [SerializeReference, SerializedDecoratorCreator(typeof(float))]
        IStatDecoratorCreateOptions statDecoratorCreateOptions;
        protected override IStatDecoratorCreateOptions<float> StatDecoratorCreateOptions => statDecoratorCreateOptions as IStatDecoratorCreateOptions<float>;
    }
}