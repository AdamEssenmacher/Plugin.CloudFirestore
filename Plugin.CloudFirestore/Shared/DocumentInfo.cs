using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Plugin.CloudFirestore
{
    internal partial class
        DocumentInfo<[DynamicallyAccessedMembers(
            DynamicallyAccessedMemberTypes.Interfaces |
            DynamicallyAccessedMemberTypes.PublicProperties |
            DynamicallyAccessedMemberTypes.PublicFields)] T> : IDocumentInfo
    {
        private readonly IDocumentInfo _objectDocumentInfo;
        private readonly IDocumentInfo? _listDocumentInfo;
        private readonly Type _type = typeof(T);

        public DocumentInfo()
        {
            if (typeof(T).TryGetImplementingGenericType(out var implementingType, typeof(IDictionary<,>)))
            {
                _objectDocumentInfo = new DictionaryDocumentInfo<T>(implementingType);
            }
            else if (typeof(IDictionary).IsAssignableFrom(_type))
            {
                _objectDocumentInfo = new DictionaryDocumentInfo<T>(typeof(IDictionary));
            }
            else if (_type.IsGenericDefinition(typeof(IReadOnlyDictionary<,>)))
            {
                _objectDocumentInfo = new DictionaryDocumentInfo<T>(_type);
            }
            else
            {
                _objectDocumentInfo = new ObjectDocumentInfo<T>();

                if (typeof(T).TryGetImplementingGenericType(out implementingType, typeof(ICollection<>)))
                {
                    _listDocumentInfo = new ListDocumentInfo<T>(implementingType);
                }
                else if (typeof(IList).IsAssignableFrom(_type))
                {
                    _listDocumentInfo = new ListDocumentInfo<T>(typeof(IList));
                }
                else if (_type.IsGenericDefinition(typeof(IEnumerable<>))
                    || _type.IsGenericDefinition(typeof(IReadOnlyCollection<>))
                    || _type.IsGenericDefinition(typeof(IReadOnlyList<>))
                    || _type == typeof(IEnumerable)
                    || _type == typeof(ICollection))
                {
                    _listDocumentInfo = new ListDocumentInfo<T>(_type);
                }
            }
        }

        public string GetMappingName(string name)
        {
            return _objectDocumentInfo.GetMappingName(name);
        }
    }
}
