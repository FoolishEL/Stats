using System;

namespace Foolish.Stats
{
    /// <summary>
    /// Base class for multiply T value on float or int
    /// </summary>
    public abstract class CrossTypeValueOperationStatDecorator<T, T2, TV2> : SingleStatDecorator<T> where TV2 : ValueProvider<T2>, new()
    {
        protected TV2 operationValueProvider;

        protected CrossTypeValueOperationStatDecorator(IStatDecorator<T> decorator, T2 initialValueProvider, bool needCyclicDispose) : base(decorator, needCyclicDispose)
        {
            operationValueProvider = new();
            operationValueProvider.Init(initialValueProvider, OnOperationValueChanged);
        }

        protected abstract void OnOperationValueChanged(T2 value);
        
        /// <summary>
        /// Set new operation value
        /// </summary>
        /// <param name="newValue"></param>
        public void ChangeOperationValue(T2 newValue) => operationValueProvider.Value =  newValue;
    }

    public abstract class ValueProvider<T>
    {
        public T Value
        {
            get => valueCached;
            set
            {
                valueCached = value;
                onValueChanged.Invoke(value);
            }
        }
        T valueCached;
        Action<T> onValueChanged;
        public void Init(T defaultValue, Action<T> onValueChanged)
        {
            this.onValueChanged = onValueChanged;
            valueCached = defaultValue;
            onValueChanged?.Invoke(defaultValue);
        }
    }

    public class FloatValueProvider : ValueProvider<float> { }

    public class IntValueProvider : ValueProvider<int> { }
}