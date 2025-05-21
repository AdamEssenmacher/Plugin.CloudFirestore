using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Plugin.CloudFirestore
{
    public static class ObjectProvider
    {
        public static void RegisterDocumentInfo<
            [DynamicallyAccessedMembers(
                DynamicallyAccessedMemberTypes.Interfaces |
                DynamicallyAccessedMemberTypes.PublicProperties |
                DynamicallyAccessedMemberTypes.PublicFields)] T>()
        {
            _documentInfos.TryAdd(typeof(T), new DocumentInfo<T>());
        }
        
        public static void RegisterListAdapterFactory<T>()
        {
            _listAdapterFactories.TryAdd(typeof(T), new ListAdapterFactory<T>());
        }

        public static void RegisterDictionaryAdapterFactory<TKey, TValue>()
        {
            _dictionaryAdapterFactories.TryAdd((typeof(TKey), typeof(TValue)), new DictionaryAdapterFactory<TKey, TValue>());
        }

        private static readonly ConcurrentDictionary<Type, IDocumentInfo> _documentInfos = new ConcurrentDictionary<Type, IDocumentInfo>();
        private static readonly ConcurrentDictionary<Type, IListAdapterFactory> _listAdapterFactories = new ConcurrentDictionary<Type, IListAdapterFactory>();
        private static readonly ConcurrentDictionary<(Type, Type), IDictionaryAdapterFactory> _dictionaryAdapterFactories = new ConcurrentDictionary<(Type, Type), IDictionaryAdapterFactory>();

        private static readonly IListAdapterFactory _listAdapterFactory = new ListAdapterFactory();
        private static readonly IDictionaryAdapterFactory _dictionaryAdapterFactory = new DictionaryAdapterFactory();

        internal static IDocumentInfo GetDocumentInfo(Type type)
        {
            if (_documentInfos.TryGetValue(type, out IDocumentInfo? documentInfo))
                return documentInfo;

            throw new InvalidOperationException($"Document type {type.FullName} has not been registered.");
        }

        internal static IDocumentInfo GetDocumentInfo<T>()
        {
            return GetDocumentInfo(typeof(T));
        }

        internal static IListAdapterFactory GetListAdapterFactory(Type type)
        {
            if (_listAdapterFactories.TryGetValue(type, out IListAdapterFactory? factory))
                return factory;

            throw new InvalidOperationException($"ListAdapterFactory type {type.FullName} has not been registered.");
        }

        internal static IListAdapterFactory GetListAdapterFactory<T>()
        {
            return GetListAdapterFactory(typeof(T));
        }

        internal static IListAdapterFactory GetListAdapterFactory()
        {
            return _listAdapterFactory;
        }

        internal static IDictionaryAdapterFactory GetDictionaryAdapterFactory(Type keyType, Type valueType)
        {
            if (_dictionaryAdapterFactories.TryGetValue((keyType, valueType), out IDictionaryAdapterFactory? factory))
                return factory;
            
            throw new InvalidOperationException($"DictionaryAdapterFactory type ({keyType.FullName}, {valueType.FullName}) has not been registered.");
        }

        internal static IDictionaryAdapterFactory GetDictionaryAdapterFactory<TKey, TValue>()
        {
            return GetDictionaryAdapterFactory(typeof(TKey), typeof(TValue));
        }

        internal static IDictionaryAdapterFactory GetDictionaryAdapterFactory()
        {
            return _dictionaryAdapterFactory;
        }
    }
}
