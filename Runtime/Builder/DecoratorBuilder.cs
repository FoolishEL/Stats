using System.Collections.Generic;

namespace Foolish.Stats.Builder
{
    /// <summary>
    /// StoreData for building custom sequence of <see cref="IStatDecorator{T}"/> chain
    /// </summary>
    /// <typeparam name="T">in general can be used any value, out of box works only with in or float.</typeparam>
    /// <remarks>For handling other types should implement custom logic
    /// like in <see cref="DecoratorBuilderExtenstion"/></remarks>
    /// 
    /// <list type="table">
    /// <item>
    /// <term>Layer</term>
    /// <description><see cref="TopLayers"/>: Decoration is build in layers.
    /// Here every layer preform some operation on previous layer.
    /// So the first layer (first <see cref="IStatDecorator{T}"/> in <see cref="TopLayers"/>) created in a special way.
    /// Like int <see cref="FromCore(T)"/></description>
    /// Also last layer is the resulting decorator.
    /// </item>
    /// </list>
    /// <example>
    /// <code>
    /// var builder = DecoratorBuilder<![CDATA[<int>]]>.FromCore(2)
    ///.DecorateWithAdditive(out IntAddValueStatDecorator intAddValueStatDecorator, 20);
    /// </code>
    /// </example>
    public class DecoratorBuilder<T>
    {
        /// <summary>
        /// Representation of ordered <see cref="IStatDecorator{T}"/> where every next layer modify its previous layer.
        /// </summary>
        /// <remarks>Modifying this list manually is not recommended</remarks>
        public readonly List<IStatDecorator<T>> TopLayers;
        
        /// <summary>
        /// Dont create DecoratorBuilder manually. Use static constructors or Extension methods instead.
        /// </summary>
        public DecoratorBuilder()
        {
            TopLayers = new();
        }
        
        /// <summary>
        /// Create DecoratorBuilder with <see cref="CoreValueProvider{T}"/> at first layer
        /// </summary>
        /// <param name="value">default value for <see cref="CoreValueProvider{T}"/></param>
        public static DecoratorBuilder<T> FromCore(T value)
        {
            var builder = new DecoratorBuilder<T>();
            builder.TopLayers.Add(new CoreValueProvider<T>(value));
            return builder;
        }

        /// <summary>
        /// Create DecoratorBuilder with <see cref="CoreValueProvider{T}"/> at first layer
        /// </summary>
        /// <param name="value">default value for <see cref="CoreValueProvider{T}"/></param>
        /// <param name="valueProvider">created <see cref="CoreValueProvider{T}"/></param>
        public static DecoratorBuilder<T> FromCore(T value, out CoreValueProvider<T> valueProvider)
        {
            var builder = new DecoratorBuilder<T>();
            valueProvider = new CoreValueProvider<T>(value);
            builder.TopLayers.Add(valueProvider);
            return builder;
        }
        
        /// <summary>
        /// Create DecoratorBuilder with <see cref="RawValueProvider{T}"/> at first layer
        /// </summary>
        /// <param name="value">default value for <see cref="RawValueProvider{T}"/></param>
        public static DecoratorBuilder<T> FromRaw(T value)
        {
            var builder = new DecoratorBuilder<T>();
            builder.TopLayers.Add(RawValueProvider<T>.FromValue(value));
            return builder;
        }
    }
}