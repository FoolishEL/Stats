using UnityEngine;

namespace Foolish.Stats.ScriptableObjectDecorators
{
    public abstract class ScriptableObjectDecorator<T> : ScriptableObject
    {
        public IStatDecorator<T> CreateDecorator() => StatDecoratorCreateOptions.CreateDecorator();

        protected abstract IStatDecoratorCreateOptions<T> StatDecoratorCreateOptions { get; }
    }
}