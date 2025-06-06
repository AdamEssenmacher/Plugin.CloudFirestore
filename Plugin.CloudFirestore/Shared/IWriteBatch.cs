﻿using System.Threading.Tasks;

namespace Plugin.CloudFirestore
{
    public interface IWriteBatch
    {
        void CommitLocal();
        Task CommitAsync();
        void SetData(IDocumentReference document, object documentData);
        void SetData(IDocumentReference document, object documentData, params string[] mergeFields);
        void SetData(IDocumentReference document, object documentData, params FieldPath[] mergeFields);
        void SetData(IDocumentReference document, object documentData, bool merge);
        void UpdateData(IDocumentReference document, object fields);
        void UpdateData(IDocumentReference document, string field, object? value, params object?[] moreFieldsAndValues);
        void UpdateData(IDocumentReference document, FieldPath field, object? value, params object?[] moreFieldsAndValues);
        void DeleteDocument(IDocumentReference document);
    }
}
