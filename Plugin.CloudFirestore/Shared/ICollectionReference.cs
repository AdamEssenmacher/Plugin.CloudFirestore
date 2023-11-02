using System;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Plugin.CloudFirestore
{
    public interface ICollectionReference : IQuery
    {
        string Id { get; }
        string Path { get; }
        IDocumentReference? Parent { get; }
        IDocumentReference CreateDocument();
        IDocumentReference GetDocument(string documentPath);
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use AddAsync<T>(T data) method instead.")]
        void AddDocument(object data, CompletionHandler handler);
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use AddAsync<T>(T data) method instead.")]
        Task AddDocumentAsync(object data);
        Task<IDocumentReference> AddAsync<T>(T data);
    }
}
