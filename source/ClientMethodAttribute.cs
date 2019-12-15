using System;

namespace ApiTools
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ClientMethodAttribute : Attribute
    {
    }
}
