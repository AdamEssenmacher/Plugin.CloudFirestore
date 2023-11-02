using System;
namespace Plugin.CloudFirestore.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class FirestoreServerTimestampAttribute : Attribute
    {
        public bool CanReplace { get; set; } = true;
    }
}
