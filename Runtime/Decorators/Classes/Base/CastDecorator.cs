namespace Foolish.Stats
{
    /// <summary>
    /// decorator that cast value from T1 into T2
    /// </summary>
    public abstract class CastDecorator<T1, T2> : AbstractStatDecorator<T2>
    {
        IStatDecorator<T1> sourceDecorator;
        protected CastDecorator(IStatDecorator<T1> sourceDecorator, bool needCyclicDispose) : base(needCyclicDispose)
        {
            this.sourceDecorator = sourceDecorator;
            sourceDecorator.OnValueChanged += OnSourceValueChanged;
            AddDecoratorToDisposable(sourceDecorator);
        }
        void OnSourceValueChanged(T1 changedValue) => Value = Convert(changedValue);

        protected abstract T2 Convert(T1 value);
        
        protected override void DisposeRaw()
        {
            sourceDecorator.OnValueChanged -= OnSourceValueChanged;
        }
    }
}