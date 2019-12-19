using System.Collections.Generic;
using System.Text;

namespace ApiTools.Codegen.Codegen
{
    public class ClientScriptsCodegen : CodegenBase
    {
        public ClientScriptsCodegen(LibraryBuilder libraryBuilder) : base(libraryBuilder)
        {
        }

        public override IEnumerable<BuildFile> Compile(BuildPackage package)
        {
            var sb = new StringBuilder();
            foreach (var type in libraryBuilder.ClientTypes)
            {
                var className = type.Identifier.ValueText;

                sb.AppendLine($"namespace {package.Namespace}");
                sb.AppendLine("{");
                sb.AppendLine($"    export class {className} extends ClientObject");
                sb.AppendLine("    {");
                sb.AppendLine();
                sb.AppendLine("    }");
                sb.AppendLine("}");

                yield return new BuildFile
                {
                    Path = className + ".ts",
                    Content = sb.ToString()
                };

                sb.Clear();
            }

            yield break;
        }
    }
}