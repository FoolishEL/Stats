using System;
using System.Collections.Generic;

namespace Foolish.Stats.Builder
{
    /// <summary>
    /// Extenstion class for <see cref="DecoratorBuilder{T}"/>
    /// </summary>
    public static class DecoratorBuilderExtenstion
    {

        /// <summary>
        /// Create Float <see cref="DecoratorBuilder{T}"/> and set first of it layer with <see cref="FloatMultipleSumStatsDecorator"/>
        /// </summary>
        /// <param name="created">created <see cref="FloatMultipleSumStatsDecorator"/></param>
        /// <param name="needCyclicDispose">does created FloatMultipleSumStatsDecorator needs to be automatically disposed</param>
        /// <param name="decorators"><see cref="IStatDecorator{T}"/>'s that will be inserted in <see cref="FloatMultipleSumStatsDecorator"/></param>
        /// <remarks>params could be skipped. IStatDecorators can be added to MultipleStatsFloatSumDecorator later at any time.</remarks>
        public static DecoratorBuilder<float> FromAggregateWithParams(out FloatMultipleSumStatsDecorator created, bool needCyclicDispose = true,
            params IStatDecorator<float>[] decorators)
        {
            var decorator = new DecoratorBuilder<float>();
            created = new(needCyclicDispose);
            if (decorators is not null && decorators.Length > 0)
            {
                foreach (var statDecorator in decorators)
                {
                    created.AddStatDecorator(statDecorator);
                }
            }

            decorator.Add(created);
            return decorator;
        }

        /// <summary>
        /// Create INT <see cref="DecoratorBuilder{T}"/> and set first of it layer with <see cref="IntMultipleSumStatsDecorator"/>
        /// </summary>
        /// <param name="created">created <see cref="IntMultipleSumStatsDecorator"/></param>
        /// <param name="needCyclicDispose">does created IntMultipleSumStatsDecorator needs to be automatically disposed</param>
        /// <param name="decorators"><see cref="IStatDecorator{T}"/>'s that will be inserted in <see cref="IntMultipleSumStatsDecorator"/></param>
        /// <remarks>params could be skipped. IStatDecorators can be added to MultipleStatsFloatSumDecorator later at any time.</remarks>
        public static DecoratorBuilder<int> FromAggregateWithParams(out IntMultipleSumStatsDecorator created, bool needCyclicDispose = true,
            params IStatDecorator<int>[] decorators)
        {
            var decorator = new DecoratorBuilder<int>();
            created = new(needCyclicDispose);
            if (decorators is not null && decorators.Length > 0)
            {
                foreach (var statDecorator in decorators)
                {
                    created.AddStatDecorator(statDecorator);
                }
            }

            decorator.Add(created);
            return decorator;
        }
        

        /// <summary>
        /// Create FLOAT <see cref="DecoratorBuilder{T}"/> from IEnumerable of <see cref="IStatDecorator{T}"/>
        /// and set them in <see cref="FloatMultipleSumStatsDecorator"/>
        /// which is set as first layer of created <see cref="DecoratorBuilder{T}"/></summary>
        /// <param name="created">created <see cref="FloatMultipleSumStatsDecorator"/></param>
        /// <param name="needCyclicDispose">does created FloatMultipleSumStatsDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<float> FromAggregateOfData(this IEnumerable<IStatDecorator<float>> data,
            out FloatMultipleSumStatsDecorator created, bool needCyclicDispose = true)
        {
            var decorator = new DecoratorBuilder<float>();
            created = new(needCyclicDispose);
            foreach (var statDecorator in data)
            {
                created.AddStatDecorator(statDecorator);
            }

            decorator.Add(created);
            return decorator;
        }

        /// <summary>
        /// Create INT <see cref="DecoratorBuilder{T}"/> from IEnumerable of <see cref="IStatDecorator{T}"/>
        /// and set them in <see cref="IntMultipleSumStatsDecorator"/>
        /// which is set as first layer of created <see cref="DecoratorBuilder{T}"/></summary>
        /// <param name="created">created <see cref="IntMultipleSumStatsDecorator"/></param>
        /// <param name="needCyclicDispose">does created IntMultipleSumStatsDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<int> FromAggregateOfData(this IEnumerable<IStatDecorator<int>> data,
            out IntMultipleSumStatsDecorator created, bool needCyclicDispose = true)
        {
            var decorator = new DecoratorBuilder<int>();
            created = new(needCyclicDispose);
            foreach (var statDecorator in data)
            {
                created.AddStatDecorator(statDecorator);
            }

            decorator.Add(created);
            return decorator;
        }

        /// <summary>
        /// Create a list of <see cref="IStatDecorator{T}"/> from IEnumerable of values
        /// </summary>
        public static List<IStatDecorator<T>> AggregateCores<T>(this IEnumerable<T> data)
        {
            List<IStatDecorator<T>> decorators = new();
            foreach (var i in data)
            {
                decorators.Add(new CoreValueProvider<T>(i));
            }

            return decorators;
        }

        /// <summary>
        /// Create a list of <see cref="IStatDecorator{T}"/> from params of values
        /// </summary>
        /// <exception cref="ArgumentException">throw if data is null or empty</exception>
        public static List<IStatDecorator<T>> AggregateCores<T>(T[] data)
        {
            if (data is null || data.Length == 0)
            {
                throw new ArgumentException("params cant be null");
            }

            List<IStatDecorator<T>> decorators = new();
            foreach (var i in data)
            {
                decorators.Add(new CoreValueProvider<T>(i));
            }

            return decorators;
        }

        /// <summary>
        /// Create new Layer to INT <see cref="DecoratorBuilder{T}"/> with <see cref="IntAddValueStatDecorator"/>
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="additiveDecorator">created <see cref="IntAddValueStatDecorator"/></param>
        /// <param name="value">default additive value</param>
        /// <param name="needCyclicDispose">does created IntAddValueStatDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<int> DecorateWithAdditive(this DecoratorBuilder<int> decoratorBuilder,
            out IntAddValueStatDecorator additiveDecorator, int value = 0, bool needCyclicDispose = true)
        {
            additiveDecorator = new(decoratorBuilder.TopLayers[^1], value, needCyclicDispose);
            decoratorBuilder.Add(additiveDecorator);
            return decoratorBuilder;
        }

        /// <summary>
        /// Create new Layer to FLOAT <see cref="DecoratorBuilder{T}"/> with <see cref="FloatAddValueStatDecorator"/>
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="additiveDecorator">created <see cref="FloatAddValueStatDecorator"/></param>
        /// <param name="value">default additive value</param>
        /// <param name="needCyclicDispose">does created FloatAddValueStatDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<float> DecorateWithAdditive(this DecoratorBuilder<float> decoratorBuilder,
                out FloatAddValueStatDecorator additiveDecorator, float value = 0f,bool needCyclicDispose = true)
        {
            additiveDecorator = new(decoratorBuilder.TopLayers[^1], value, needCyclicDispose);
            decoratorBuilder.Add(additiveDecorator);
            return decoratorBuilder;
        }

        /// <summary>
        /// Create new Layer to INT <see cref="DecoratorBuilder{T}"/> with <see cref="IntMultiplyIntValueStatDecorator"/>
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="multiplyIntStatDecorator">created <see cref="IntMultiplyIntValueStatDecorator"/></param>
        /// <param name="multiplyValue">default multiply value</param>
        /// <param name="needCyclicDispose">does created IntMultiplyIntValueStatDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<int> DecorateWithMultiply(this DecoratorBuilder<int> decoratorBuilder,
            out IntMultiplyIntValueStatDecorator multiplyIntStatDecorator, int multiplyValue = 1, bool needCyclicDispose = true)
        {
            multiplyIntStatDecorator = new(decoratorBuilder.TopLayers[^1], multiplyValue, needCyclicDispose);
            decoratorBuilder.Add(multiplyIntStatDecorator);
            return decoratorBuilder;
        }
        
        /// <summary>
        /// Create new Layer to INT <see cref="DecoratorBuilder{T}"/> with <see cref="IntMultiplyFloatValueStatDecorator"/>
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="multiplyIntStatDecorator">created <see cref="IntMultiplyFloatValueStatDecorator"/></param>
        /// <param name="multiplyValue">default multiply value</param>
        /// <param name="needCyclicDispose">does created IntMultiplyFloatValueStatDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<int> DecorateWithMultiply(this DecoratorBuilder<int> decoratorBuilder,
            out IntMultiplyFloatValueStatDecorator multiplyIntStatDecorator, float multiplyValue = 1, bool needCyclicDispose = true)
        {
            multiplyIntStatDecorator = new(decoratorBuilder.TopLayers[^1], multiplyValue, needCyclicDispose);
            decoratorBuilder.Add(multiplyIntStatDecorator);
            return decoratorBuilder;
        }
        
        /// <summary>
        /// Create new Layer to FLOAT <see cref="DecoratorBuilder{T}"/> with <see cref="FloatMultiplyFloatValueStatDecorator"/>
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="multiplyFloatStatDecorator">created <see cref="FloatMultiplyFloatValueStatDecorator"/></param>
        /// <param name="multiplyValue">default multiply value</param>
        /// <param name="needCyclicDispose">does created FloatMultiplyFloatValueStatDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<float> DecorateWithMultiply(this DecoratorBuilder<float> decoratorBuilder,
            out FloatMultiplyFloatValueStatDecorator multiplyFloatStatDecorator, float multiplyValue = 1f, bool needCyclicDispose = true)
        {
            multiplyFloatStatDecorator = new(decoratorBuilder.TopLayers[^1], multiplyValue, needCyclicDispose);
            decoratorBuilder.Add(multiplyFloatStatDecorator);
            return decoratorBuilder;
        }
        
        /// <summary>
        /// Create new Layer to FLOAT <see cref="DecoratorBuilder{T}"/> with <see cref="FloatMultiplyIntValueStatDecorator"/>
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="multiplyFloatStatDecorator">created <see cref="FloatMultiplyIntValueStatDecorator"/></param>
        /// <param name="multiplyValue">default multiply value</param>
        /// <param name="needCyclicDispose">does created FloatMultiplyIntValueStatDecorator needs to be automatically disposed</param>
        public static DecoratorBuilder<float> DecorateWithMultiply(this DecoratorBuilder<float> decoratorBuilder,
            out FloatMultiplyIntValueStatDecorator multiplyFloatStatDecorator, int multiplyValue = 1, bool needCyclicDispose = true)
        {
            multiplyFloatStatDecorator = new(decoratorBuilder.TopLayers[^1], multiplyValue, needCyclicDispose);
            decoratorBuilder.Add(multiplyFloatStatDecorator);
            return decoratorBuilder;
        }

        /// <summary>
        /// Create new Layer to INT <see cref="DecoratorBuilder{T}"/> with <see cref="IntMultipleSumStatsDecorator"/>
        /// Always add last <see cref="IStatDecorator{T}"/> form DecoratorBuilder Layer to IntMultipleSumStatsDecorator.
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="created">created <see cref="IntMultipleSumStatsDecorator"/></param>
        /// <param name="needCyclicDispose">does created IntMultipleSumStatsDecorator needs to be automatically disposed</param>
        /// <param name="additional">if need to add some other <see cref="IStatDecorator{T}"/>s in <see cref="IntMultipleSumStatsDecorator"/></param>
        /// <remarks>
        /// additional IStatDecorator can be added to created <see cref="IntMultipleSumStatsDecorator"/> later.
        /// </remarks>
        public static DecoratorBuilder<int> DecorateWithMultipleSum(this DecoratorBuilder<int> decoratorBuilder,
            out IntMultipleSumStatsDecorator created, bool needCyclicDispose = true, params IStatDecorator<int>[] additional)
        {
            created = new(needCyclicDispose);
            created.AddStatDecorator(decoratorBuilder.Last());
            if (additional is not null && additional.Length > 0)
            {
                foreach (var statDecorator in additional)
                {
                    created.AddStatDecorator(statDecorator);
                }
            }
            decoratorBuilder.Add(created);
            return decoratorBuilder;
        }

        /// <summary>
        /// Create new Layer to FLOAT <see cref="DecoratorBuilder{T}"/> with <see cref="FloatMultipleSumStatsDecorator"/>
        /// Always add last <see cref="IStatDecorator{T}"/> form DecoratorBuilder Layer to FloatMultipleSumStatsDecorator.
        /// </summary>
        /// <param name="decoratorBuilder">builder to be modified</param>
        /// <param name="created">created <see cref="FloatMultipleSumStatsDecorator"/></param>
        /// <param name="needCyclicDispose">does created FloatMultipleSumStatsDecorator needs to be automatically disposed</param>
        /// <param name="additional">if need to add some other <see cref="IStatDecorator{T}"/>s in <see cref="FloatMultipleSumStatsDecorator"/></param>
        /// <remarks>
        /// additional IStatDecorator can be added to created <see cref="FloatMultipleSumStatsDecorator"/> later.
        /// </remarks>
        public static DecoratorBuilder<float> DecorateWithMultipleSum(this DecoratorBuilder<float> decoratorBuilder,
            out FloatMultipleSumStatsDecorator created, bool needCyclicDispose = true, params IStatDecorator<float>[] additional)
        {
            created = new(needCyclicDispose);
            created.AddStatDecorator(decoratorBuilder.Last());
            if (additional is not null && additional.Length > 0)
            {
                foreach (var statDecorator in additional)
                {
                    created.AddStatDecorator(statDecorator);
                }
            }
            decoratorBuilder.Add(created);
            return decoratorBuilder;
        }

        /// <summary>
        /// Return resulting <see cref="IStatDecorator{T}"/> of current builder
        /// </summary>
        /// <param name="builder">builder to be modified</param>
        /// <param name="disposable">return last decorator as IDisposable</param>
        public static IStatDecorator<T> Build<T>(this DecoratorBuilder<T> builder, out IDisposable disposable)
        {
            disposable = null;
            var lastDecorator = builder.Last();
            if (lastDecorator is IDisposable disposableDecorator)
            {
                disposable = disposableDecorator;
            }
            return lastDecorator;
        }
        
        static void Add<T>(this DecoratorBuilder<T> decoratorBuilder, IStatDecorator<T> decorator)
        {
            decoratorBuilder.TopLayers.Add(decorator);
        }

        static IStatDecorator<T> Last<T>(this DecoratorBuilder<T> decoratorBuilder)
        {
            return decoratorBuilder.TopLayers[^1];
        }
    }
}
