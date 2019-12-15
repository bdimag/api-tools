using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ApiTools.Codegen
{
    public class BuildFile
    {
        /// <summary>
        /// The path and filename of this file, relative to the root of the project.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Write the contents of this file to the specified <see cref="TextWriter"/>.
        /// </summary>
        public void Write(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class ClientMemberInfo
    {
        public Documentation Documentation { get; set; }

        public string Name { get; set; }
    }

    public class ClientMethod : ClientMemberInfo
    {
        public bool IsStatic { get; set; }
    }

    public class ClientProperty : ClientMemberInfo
    {
        public bool IsStatic { get; set; }
    }

    public class ClientType : ClientMemberInfo
    {
        public ClientMethod Constructor { get; set; }

        public string Id { get; set; }

        public IEnumerable<ClientMethod> Methods { get; set; }

        public IEnumerable<ClientProperty> Properties { get; set; }
    }

    public class Documentation
    {
        public Dictionary<string, string> Params { get; set; }

        public string Returns { get; set; }

        public string Summary { get; set; }
    }

    public sealed class BuildPackage
    {
        public string Name { get; set; }

        public string Namespace { get; set; }
    }


    public sealed class LibraryBuilder : IDisposable
    {
        private List<ClientType> clientTypes = new List<ClientType>();

        private T[] GetMembers<T>(Type type) where T : ClientMemberInfo
        {
            throw new NotImplementedException();
        }

        internal ClientType BuildType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsDefined(typeof(ClientTypeAttribute)))
            {
                throw new InvalidOperationException($"Type must have the {nameof(ClientTypeAttribute)} applied.");
            }

            var clientType = new ClientType();

            // generate methods
            foreach (var m in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                // check for presence of the appropriate attribute
                if (!m.IsDefined(typeof(ClientMethodAttribute)))
                    continue;

                if (m.IsStatic)
                {
                }
                else
                {
                }
            }

            return clientType;
        }

        public void Add(params Type[] types) => clientTypes.AddRange(types.Select(type => BuildType(type)));

        public IEnumerable<BuildFile> Build()
        {
            return Enumerable.Empty<BuildFile>();
        }

        public void Dispose()
        {
        }
    }
}