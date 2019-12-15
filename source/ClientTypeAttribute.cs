using System;

namespace ApiTools
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ClientTypeAttribute : Attribute
    {
        public string Id { get; }

        public ClientTypeAttribute(string id)
        {
            Id = id;
        }
    }
}
