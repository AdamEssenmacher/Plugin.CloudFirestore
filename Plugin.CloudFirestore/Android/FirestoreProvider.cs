using System;
using System.Collections.Concurrent;
using Firebase;
using Firebase.Firestore;

namespace Plugin.CloudFirestore
{
    internal static class FirestoreProvider
    {
        private static ConcurrentDictionary<FirebaseFirestore, Lazy<FirebaseFirestoreWrapper>> _firestores = new ConcurrentDictionary<FirebaseFirestore, Lazy<FirebaseFirestoreWrapper>>();

        public static FirebaseFirestoreWrapper FirebaseFirestore { get; } = new FirebaseFirestoreWrapper(Firebase.Firestore.FirebaseFirestore.Instance);

        public static FirebaseFirestoreWrapper GetFirestore(string appName)
        {
            var app = FirebaseApp.GetInstance(appName);
            return GetFirestore(Firebase.Firestore.FirebaseFirestore.GetInstance(app));
        }

        public static FirebaseFirestoreWrapper GetFirestore(FirebaseFirestore firestore)
        {
            if (firestore == Firebase.Firestore.FirebaseFirestore.Instance)
            {
                return FirebaseFirestore;
            }
            return _firestores.GetOrAdd(firestore, key => new Lazy<FirebaseFirestoreWrapper>(() => new FirebaseFirestoreWrapper(key))).Value;
        }
    }
}
