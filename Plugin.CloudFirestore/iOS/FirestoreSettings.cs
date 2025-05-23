namespace Plugin.CloudFirestore
{
    public partial class FirestoreSettings
    {
        static FirestoreSettings()
        {
            var settings = new Firebase.CloudFirestore.FirestoreSettings();
            _defaultHost = settings.Host;
            _defaultIsSslEnabled = settings.SslEnabled;
        }

        internal FirestoreSettings(Firebase.CloudFirestore.FirestoreSettings settings)
        {
            Host = settings.Host;
            IsSslEnabled = settings.SslEnabled;
        }
    }
}
