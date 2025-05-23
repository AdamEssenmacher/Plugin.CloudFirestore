namespace Plugin.CloudFirestore
{
    public partial class FirestoreSettings
    {
        static FirestoreSettings()
        {
            var settings = new Firebase.Firestore.FirebaseFirestoreSettings.Builder().Build();
            _defaultHost = settings.Host;
            _defaultIsSslEnabled = settings.IsSslEnabled;
        }

        internal FirestoreSettings(Firebase.Firestore.FirebaseFirestoreSettings settings)
        {
            Host = settings.Host;
            IsSslEnabled = settings.IsSslEnabled;
        }
    }
}
