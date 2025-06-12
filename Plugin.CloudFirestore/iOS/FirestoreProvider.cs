using System;
using System.Collections.Concurrent;
using Firebase.CloudFirestore;

namespace Plugin.CloudFirestore
{
    internal static class FirestoreProvider
    {
        private static ConcurrentDictionary<Firestore, Lazy<FirebaseFirestoreWrapper>> _firestores = new ConcurrentDictionary<Firestore, Lazy<FirebaseFirestoreWrapper>>();

        public static FirebaseFirestoreWrapper FirebaseFirestore { get; } = new FirebaseFirestoreWrapper(Firebase.CloudFirestore.Firestore.SharedInstance);

        public static FirebaseFirestoreWrapper GetFirestore(string appName)
        {
            var app = Firebase.Core.App.From(appName);
            if (app == null)
                throw new InvalidOperationException("Firestore app not found");
            return GetFirestore(Firebase.CloudFirestore.Firestore.Create(app));
        }

        public static FirebaseFirestoreWrapper GetFirestore(Firestore firestore)
        {
            if (firestore == Firebase.CloudFirestore.Firestore.SharedInstance)
            {
                return FirebaseFirestore;
            }
            return _firestores.GetOrAdd(firestore, key => new Lazy<FirebaseFirestoreWrapper>(() => new FirebaseFirestoreWrapper(key))).Value;
        }
    }
}
