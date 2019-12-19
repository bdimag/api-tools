using ApiTools.Codegen.Codegen;
using ApiTools.Codegen.Docs;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiTools.Codegen
{
    public sealed class LibraryBuilder : IDisposable
    {
        private List<ClassDeclarationSyntax> clientTypes = new List<ClassDeclarationSyntax>();

        private List<Documentation> documentation = new List<Documentation>();

        private Dictionary<ProjectType, CodegenBase> generators = new Dictionary<ProjectType, CodegenBase>();

        internal ICollection<ClassDeclarationSyntax> ClientTypes
        {
            get
            {
                return clientTypes.AsReadOnly();
            }
        }

        public LibraryBuilder()
        {
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

        public void AddFile(Stream fileStream)
        {
            using (var reader = new StreamReader(fileStream))
            {
                var textContent = reader.ReadToEnd();
                var tree = CSharpSyntaxTree.ParseText(textContent);

                foreach (var classDeclaration in tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>())
                {
                    var newClassDeclaration = SyntaxFactory.ClassDeclaration(classDeclaration.Identifier.ValueText);

                    foreach (var node in classDeclaration.ChildNodes().OfType<MemberDeclarationSyntax>())
                    {

                        if (node is MethodDeclarationSyntax method)
                        {
                            var newMethod = SyntaxFactory.MethodDeclaration(method.ReturnType, method.Identifier.ValueText);

                            newClassDeclaration.AddMembers(newMethod).NormalizeWhitespace();
                        }
                        else if (node is PropertyDeclarationSyntax property)
                        {
                            var newProperty = SyntaxFactory.PropertyDeclaration(property.Type, property.Identifier.ValueText);

                            newClassDeclaration.AddMembers(newProperty).NormalizeWhitespace();
                        }
                    }

                    newClassDeclaration.AddMembers(SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("System.Object"), "Path"));

                    clientTypes.Add(newClassDeclaration);
                }
            }
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

    internal static class SyntaxExtensions
    {
        internal static bool HasClientAttribute(this MemberDeclarationSyntax memberDeclaration)
        {
            // TODO

            foreach (var attribute in memberDeclaration.AttributeLists.SelectMany(list => list.Attributes))
            {
                string name = null;

                var qualifiedName = attribute.ChildNodes().OfType<QualifiedNameSyntax>().FirstOrDefault();
                if (qualifiedName != null)
                {
                    name = qualifiedName.Right.Identifier.ValueText;
                    
                }

                var identifier = attribute.ChildNodes().OfType<IdentifierNameSyntax>().FirstOrDefault();
                if (identifier != null)
                {
                    name = identifier.Identifier.ValueText;
                }

                if (name  == "ClientMethod" || name == "ClientProperty")
                {
                    return true;
                }
            }

            return false;
        }

        internal static ClientTypeAttribute GetClientAttribute(this ClassDeclarationSyntax classDeclaration)
        {
            throw new NotImplementedException();
        }

        
    }
}