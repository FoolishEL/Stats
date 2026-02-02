namespace Foolish.Stats.ScriptableObjectDecorators
{
    public interface IStatDecoratorCreateOptions
    {

    }

    public interface IStatDecoratorCreateOptions<T> : IStatDecoratorCreateOptions
    {
        IStatDecorator<T> CreateDecorator();
    }
}