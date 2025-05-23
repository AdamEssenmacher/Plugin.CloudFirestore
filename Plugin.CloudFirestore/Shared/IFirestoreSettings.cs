namespace Plugin.CloudFirestore
{
    public interface IFirestoreSettings
    {
        string Host { get; }
        bool IsSslEnabled { get; }
    }
}
