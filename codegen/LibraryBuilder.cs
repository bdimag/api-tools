using ApiTools.Codegen.Codegen;
using ApiTools.Codegen.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApiTools.Codegen
{
    public sealed class LibraryBuilder : IDisposable
    {
        private List<ClientType> clientTypes = new List<ClientType>();

        private List<Documentation> documentation = new List<Documentation>();

        private Dictionary<ProjectType, CodegenBase> generators = new Dictionary<ProjectType, CodegenBase>();

        internal ICollection<ClientType> ClientTypes
        {
            get
            {
                return clientTypes.AsReadOnly();
            }
        }

        public LibraryBuilder(IEnumerable<Type> types, Documentation documentation)
        {
            clientTypes.AddRange(types.Select(type => BuildType(type)));

            this.documentation.Add(documentation);
        }

        private CodegenBase GetDefaultGenerator(ProjectType projectType)
        {
            if (!generators.TryGetValue(projectType, out var generator))
            {
                switch (projectType)
                {
                    case ProjectType.ClientLibrary:
                        generators.Add(projectType, generator = new ClientLibraryCodegen(this));
                        break;

                    case ProjectType.ClientScripts:
                        generators.Add(projectType, generator = new ClientScriptsCodegen(this));
                        break;

                    case ProjectType.WebApi:
                        generators.Add(projectType, generator = new WebApiCodegen(this));
                        break;
                }
            }

            return generator;
        }

        private T[] GetMembers<T>(Type type, MemberTypes filter = MemberTypes.All) where T : ClientMemberInfo
        {
            var members = new List<T>();
            foreach (var member in type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                if (typeof(T) == typeof(ClientMethod) && member is MethodInfo method)
                {
                    // check for presence of the appropriate attribute
                    if (!method.IsDefined(typeof(ClientMethodAttribute)))
                        continue;

                    members.Add(new ClientMethod()
                    {
                        IsStatic = method.IsStatic,
                        Name = method.Name
                    } as T);
                }
            }

            return members.ToArray();
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

            var clientType = new ClientType()
            {
                Name = type.Name,
                Constructors = GetMembers<ClientMethod>(type, MemberTypes.Constructor),
                Methods = GetMembers<ClientMethod>(type),
                Properties = GetMembers<ClientProperty>(type)
            };

            return clientType;
        }

        public IEnumerable<BuildFile> Build(ProjectType projectType, string name, string ns)
        {
            return GetDefaultGenerator(projectType).Compile(new BuildPackage
            {
                Name = name,
                Namespace = ns
            });
        }

        public void Dispose()
        {
        }
    }
}