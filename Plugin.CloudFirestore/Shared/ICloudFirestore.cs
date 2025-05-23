namespace Plugin.CloudFirestore
{
    public interface ICloudFirestore
    {
        IFirebaseFirestore Instance { get; }
        IFirebaseFirestore GetInstance(string appName);
        void SetLoggingEnabled(bool loggingEnabled);
    }
}
