using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Codegen.Codegen
{
    public class ClientLibraryCodegen : CodegenBase
    {
        public ClientLibraryCodegen(LibraryBuilder libraryBuilder) : base(libraryBuilder)
        {
        }

        public override IEnumerable<BuildFile> Compile(BuildPackage package)
        {
            foreach (var type in libraryBuilder.ClientTypes)
            {
                var className = type.Identifier.ValueText;

                var ns = CSharpSyntaxTree.ParseText($"namespace {package.Namespace} {{ }}")
                    .GetRoot()
                    .DescendantNodes().OfType<NamespaceDeclarationSyntax>().First();

                var result = ns.AddMembers(type).NormalizeWhitespace();

                yield return new BuildFile
                {
                    Path = className + ".cs",
                    Content = result.ToString()
                };
            }

            yield break;
        }
    }
}