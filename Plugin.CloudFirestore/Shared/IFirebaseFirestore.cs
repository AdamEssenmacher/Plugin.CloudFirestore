using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Plugin.CloudFirestore
{
    public interface IFirebaseFirestore
    {
        IFirestoreSettings FirestoreSettings { get; set; }
        ICollectionReference GetCollection(string collectionPath);
        IDocumentReference GetDocument(string documentPath);
        IQuery GetCollectionGroup(string collectionId);
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use RunTransactionAsync<T>(TransactionHandler<T> handler) method instead.")]
        void RunTransaction<T>(TransactionHandler<T> handler, CompletionHandler<T> completionHandler);
        Task<T> RunTransactionAsync<T>(TransactionHandler<T> handler);
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use RunTransactionAsync(TransactionHandler handler) method instead.")]
        void RunTransaction(TransactionHandler handler, CompletionHandler completionHandler);
        Task RunTransactionAsync(TransactionHandler handler);
        IWriteBatch CreateBatch();
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use EnableNetworkAsync() method instead.")]
        void EnableNetwork(CompletionHandler handler);
        Task EnableNetworkAsync();
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Please use DisableNetworkAsync() method instead.")]
        void DisableNetwork(CompletionHandler handler);
        Task DisableNetworkAsync();
        IListenerRegistration AddSnapshotsInSyncListener(Action listener);
        Task ClearPersistenceAsync();
        Task TerminateAsync();
        Task WaitForPendingWritesAsync();
    }
}
