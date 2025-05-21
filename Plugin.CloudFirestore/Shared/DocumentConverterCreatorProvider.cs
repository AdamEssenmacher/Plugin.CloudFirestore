using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Plugin.CloudFirestore.Converters;

namespace Plugin.CloudFirestore
{
    public class DocumentConverterCreatorProvider
    {
        private static readonly ConcurrentDictionary<Type, object?> DefaultValues = new();

        public static void RegisterDefault<T>(T? defaultValue = default)
        {
            DefaultValues.AddOrUpdate(typeof(T), defaultValue, (_, _) => defaultValue);
        }

        private static class CreatorCache<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>
        {
            public static readonly Func<Type, object?[]?, DocumentConverter> Instance = CreateCreator(typeof(T));

            private static Func<Type, object?[]?, DocumentConverter> CreateCreator(
                [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type)
            {
                var argumentTypes = GetGenericArguments(type);

                Func<Type, object?[]?, DocumentConverter> result;
                var targetType = Expression.Parameter(typeof(Type), "targetType");
                var parameters = Expression.Parameter(typeof(object[]), "args");

                if (argumentTypes == null)
                {
                    var constructor = type.GetConstructor(new[] { typeof(Type) });
                    result = Expression.Lambda<Func<Type, object?[]?, DocumentConverter>>(
                        Expression.New(constructor, targetType), targetType, parameters).Compile();
                }
                else
                {
                    var constructor = type.GetConstructor(new[] { typeof(Type) }.Concat(argumentTypes).ToArray());

                    var expressons = new Expression[] { targetType }.Concat(
                        argumentTypes.Select((type, i) => Expression.Convert(Expression.ArrayIndex(parameters, Expression.Constant(i)), type)));

                    var creator = Expression.Lambda<Func<Type, object?[]?, DocumentConverter>>(
                        Expression.New(constructor, expressons), targetType, parameters).Compile();

                    foreach(var argType in argumentTypes)
                    {
                        if (argType.IsValueType && !DefaultValues.ContainsKey(argType))
                        {
                            throw new InvalidOperationException($"No default value registered for type {argType.FullName}");
                        }
                    }

                    object?[] defaultValues = argumentTypes
                        .Select(t => t.IsValueType ? DefaultValues[t] : null)
                        .ToArray();

                    result = (targetType, parameters) => creator(targetType,
                        argumentTypes.Select((type, i) => parameters == null || i >= parameters.Length
                            ? defaultValues[i]
                            : parameters[i] != null ? Convert.ChangeType(parameters[i], Nullable.GetUnderlyingType(type) ?? type) : parameters[i])
                        .ToArray());
                }
                return result;
            }

            private static Type[]? GetGenericArguments(Type type)
            {
                if (type.BaseType == null) return null;

                if (type.IsGenericType)
                {
                    var definition = type.GetGenericTypeDefinition();
                    if (definition == typeof(DocumentConverter<>)
                        || definition == typeof(DocumentConverter<,>)
                        || definition == typeof(DocumentConverter<,,>)
                        || definition == typeof(DocumentConverter<,,,>)
                        || definition == typeof(DocumentConverter<,,,,>)
                        || definition == typeof(DocumentConverter<,,,,,>))
                    {
                        return type.GetGenericArguments();
                    }
                }
                return GetGenericArguments(type.BaseType);
            }
        }

        private static ConcurrentDictionary<Type, Func<Type, object?[]?, DocumentConverter>> _creators = new ConcurrentDictionary<Type, Func<Type, object?[]?, DocumentConverter>>();

        public static void RegisterCreator<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
        {
            _creators.TryAdd(typeof(T), CreatorCache<T>.Instance);
        }

        internal static Func<Type, object?[]?, DocumentConverter> GetCreator(Type type)
        {
            return _creators.GetOrAdd(type, GetCreatorCache);
        }

        internal static Func<Type, object?[]?, DocumentConverter> GetCreator<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
        {
            return CreatorCache<T>.Instance;
        }

        private static Func<Type, object?[]?, DocumentConverter> GetCreatorCache(Type type)
        {
            if (_creators.TryGetValue(type, out Func<Type, object?[]?, DocumentConverter>? creator))
                return creator;

            throw new InvalidOperationException($"No creator for type {type.FullName}");
        }
    }
}
