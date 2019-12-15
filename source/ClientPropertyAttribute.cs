using System;

namespace ApiTools
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ClientPropertyAttribute : Attribute
    {
    }
}
