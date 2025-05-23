using Firebase.Firestore;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.CloudFirestore
{
    internal static class DocumentMapper
    {
        public static IDictionary<string, object?>? Map(DocumentSnapshot source, ServerTimestampBehavior? serverTimestampBehavior = null)
        {
            if (source.Exists())
            {
                IDictionary<string, Java.Lang.Object> data;
                if (serverTimestampBehavior == null)
                {
                    data = source.Data;
                }
                else
                {
                    data = source.GetData(serverTimestampBehavior.Value.ToNative());
                }

                var fieldInfo = new DocumentFieldInfo<object?>();
                return data.ToDictionary(pair => pair.Key, pair => pair.Value.ToFieldValue(fieldInfo));
            }
            return null;
        }

        public static T? Map<T>(DocumentSnapshot source, ServerTimestampBehavior? serverTimestampBehavior = null)
        {
            var value = ObjectProvider.GetDocumentInfo<T>().Create(source, serverTimestampBehavior);
            return value is not null ? (T)value : default;
        }
    }
}
