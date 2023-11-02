using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Firebase.CloudFirestore;
using Foundation;

namespace Plugin.CloudFirestore
{
    public class CloudFirestoreImplementation : ICloudFirestore
    {
        public IFirebaseFirestore Instance => FirestoreProvider.FirebaseFirestore;

        public IFirebaseFirestore GetInstance(string appName)
        {
            return FirestoreProvider.GetFirestore(appName);
        }

        public void SetLoggingEnabled(bool loggingEnabled)
        {
            Firestore.EnableLogging(loggingEnabled);
        }
    }
}