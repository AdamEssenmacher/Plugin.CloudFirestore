using System;
namespace Plugin.CloudFirestore
{
    public partial class FirestoreSettings : IFirestoreSettings, IEquatable<FirestoreSettings>
    {
        private static string? _defaultHost;
        private static bool _defaultIsSslEnabled;

        public string Host { get; set; }
        public bool IsSslEnabled { get; set; }

        public FirestoreSettings()
        {
            Host = _defaultHost!;
            IsSslEnabled = _defaultIsSslEnabled;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as FirestoreSettings);
        }

        public bool Equals(FirestoreSettings? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return Host == other.Host
                   && IsSslEnabled == other.IsSslEnabled;
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return (Host.GetHashCode())
                   ^ IsSslEnabled.GetHashCode();
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }
    }
}