using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Plugin.CloudFirestore
{
    internal static class ObjectProvider
    {
        private static class DocumentInfoCache<T>
        {
            public static readonly IDocumentInfo Instance = new DocumentInfo<T>();
        }

        private static class ListAdapterFactoryCache<T>
        {
            public static readonly IListAdapterFactory Instance = new ListAdapterFactory<T>();
        }

        private static class DictionaryAdapterFactoryCache<TKey, TValue>
        {
            public static readonly IDictionaryAdapterFactory Instance = new DictionaryAdapterFactory<TKey, TValue>();
        }

        private static readonly ConcurrentDictionary<Type, IDocumentInfo> DocumentInfos = new();
        private static readonly ConcurrentDictionary<Type, IListAdapterFactory> ListAdapterFactories = new();
        private static readonly ConcurrentDictionary<(Type, Type), IDictionaryAdapterFactory> DictionaryAdapterFactories = new();

        private static readonly IListAdapterFactory ListAdapterFactory = new ListAdapterFactory();
        private static readonly IDictionaryAdapterFactory DictionaryAdapterFactory = new DictionaryAdapterFactory();

        public static IDocumentInfo GetDocumentInfo(Type documentType)
        {
            return DocumentInfos.GetOrAdd(documentType, GetDocumentInfoCache);
        }

        public static IDocumentInfo GetDocumentInfo<T>()
        {
            return DocumentInfoCache<T>.Instance;
        }

        public static IListAdapterFactory GetListAdapterFactory(Type type)
        {
            return ListAdapterFactories.GetOrAdd(type, GetListAdapterFactoryCache);
        }

        public static IListAdapterFactory GetListAdapterFactory<T>()
        {
            return ListAdapterFactoryCache<T>.Instance;
        }

        public static IListAdapterFactory GetListAdapterFactory()
        {
            return ListAdapterFactory;
        }

        public static IDictionaryAdapterFactory GetDictionaryAdapterFactory(Type keyType, Type valueType)
        {
            return DictionaryAdapterFactories.GetOrAdd((keyType, valueType), GetDictionaryAdapterFactoryCache);
        }

        public static IDictionaryAdapterFactory GetDictionaryAdapterFactory<TKey, TValue>()
        {
            return DictionaryAdapterFactoryCache<TKey, TValue>.Instance;
        }

        public static IDictionaryAdapterFactory GetDictionaryAdapterFactory()
        {
            return DictionaryAdapterFactory;
        }

        private static IDocumentInfo GetDocumentInfoCache(Type type)
        {
            return (IDocumentInfo)typeof(DocumentInfoCache<>).MakeGenericType(type)
                .GetField("Instance", BindingFlags.Public | BindingFlags.Static)!
                .GetValue(null)!;
        }

        private static IListAdapterFactory GetListAdapterFactoryCache(Type type)
        {
            return (IListAdapterFactory)typeof(ListAdapterFactoryCache<>).MakeGenericType(type)
                .GetField("Instance", BindingFlags.Public | BindingFlags.Static)!
                .GetValue(null)!;
        }

        private static IDictionaryAdapterFactory GetDictionaryAdapterFactoryCache((Type key, Type value) type)
        {
            return (IDictionaryAdapterFactory)typeof(DictionaryAdapterFactoryCache<,>).MakeGenericType(type.key, type.value)
                .GetField("Instance", BindingFlags.Public | BindingFlags.Static)!
                .GetValue(null)!;
        }
    }
}
