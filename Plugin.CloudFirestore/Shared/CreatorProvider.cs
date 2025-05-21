using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Plugin.CloudFirestore
{
    public static class CreatorProvider
    {
        private static class CreatorCache<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>
        {
            public static readonly Func<object> Instance = CreateCreator(typeof(T));

            private static Func<object> CreateCreator(
                [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type type)
            {
                return () => Activator.CreateInstance(type)!;
            }
        }

        public static void Register<T>() where T : new()
        {
            Creators[typeof(T)] = CreatorCache<T>.Instance;
        }

        private static readonly ConcurrentDictionary<Type, Func<object>> Creators = new();

        internal static Func<object> GetCreator(Type type)
        {
            if (Creators.TryGetValue(type, out Func<object>? creator))
                return creator;

            throw new InvalidOperationException($"No creator registered for {type.FullName}. Call CreatorProvider.Register<T>() at startup.");
        }

        internal static Func<object> GetCreator<T>()
        {
            return GetCreator(typeof(T));
        }
    }
}
