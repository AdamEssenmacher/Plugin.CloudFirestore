using System;
namespace Plugin.CloudFirestore.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class FirestorePropertyAttribute : Attribute
    {
        public FirestorePropertyAttribute(string mapping)
        {
            Mapping = mapping;
        }

        public string Mapping { get; }
    }
}
