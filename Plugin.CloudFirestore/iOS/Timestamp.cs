using Foundation;

namespace Plugin.CloudFirestore
{
    public partial struct Timestamp
    {
        internal Timestamp(Firebase.Core.Timestamp timestamp)
        {
            Seconds = timestamp.Seconds;
            Nanoseconds = timestamp.Nanoseconds;
        }

        internal Timestamp(NSDate date) : this(Firebase.Core.Timestamp.Create(date))
        {
        }

        internal Firebase.Core.Timestamp ToNative()
        {
            return new Firebase.Core.Timestamp(Seconds, Nanoseconds);
        }
    }
}
