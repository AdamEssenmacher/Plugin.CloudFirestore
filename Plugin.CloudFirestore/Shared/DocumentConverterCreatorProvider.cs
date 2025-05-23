using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Plugin.CloudFirestore.Converters;

namespace Plugin.CloudFirestore
{
    internal class DocumentConverterCreatorProvider
    {
        private static class CreatorCache<T>
        {
            public static readonly Func<Type, object?[]?, DocumentConverter> Instance = CreateCreator(typeof(T));

            private static Func<Type, object?[]?, DocumentConverter> CreateCreator(Type type)
            {
                var argumentTypes = GetGenericArguments(type);

                Func<Type, object?[]?, DocumentConverter> result;
                var targetType = Expression.Parameter(typeof(Type), "targetType");
                var parameters = Expression.Parameter(typeof(object[]), "args");

                if (argumentTypes == null)
                {
                    var constructor = type.GetConstructor(new[] { typeof(Type) });
                    result = Expression.Lambda<Func<Type, object?[]?, DocumentConverter>>(
                        Expression.New(constructor!, targetType), targetType, parameters).Compile();
                }
                else
                {
                    var constructor = type.GetConstructor(new[] { typeof(Type) }.Concat(argumentTypes).ToArray());

                    var expressons = new Expression[] { targetType }.Concat(
                        argumentTypes.Select((t, i) => Expression.Convert(Expression.ArrayIndex(parameters, Expression.Constant(i)), t)));

                    var creator = Expression.Lambda<Func<Type, object?[]?, DocumentConverter>>(
                        Expression.New(constructor!, expressons), targetType, parameters).Compile();

                    var defaultValues = Expression.Lambda<Func<object[]>>(
                        Expression.NewArrayInit(typeof(object), argumentTypes.Select(t =>
                            Expression.Convert(Expression.Default(t), typeof(object))))).Compile().Invoke();

                    result = (target, p) => creator(target,
                        argumentTypes.Select((t, i) => p == null || i >= p.Length
                            ? defaultValues[i]
                            : p[i] != null ? Convert.ChangeType(p[i], Nullable.GetUnderlyingType(t) ?? t) : p[i])
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

        public static Func<Type, object?[]?, DocumentConverter> GetCreator(Type type)
        {
            return _creators.GetOrAdd(type, GetCreatorCache);
        }

        public static Func<Type, object?[]?, DocumentConverter> GetCreator<T>()
        {
            return CreatorCache<T>.Instance;
        }

        private static Func<Type, object?[]?, DocumentConverter> GetCreatorCache(Type type)
        {
            return (Func<Type, object?[]?, DocumentConverter>)typeof(CreatorCache<>).MakeGenericType(type)
                .GetField("Instance", BindingFlags.Public | BindingFlags.Static)!
                .GetValue(null)!;
        }
    }
}
