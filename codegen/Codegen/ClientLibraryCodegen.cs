using System.Collections.Generic;
using System.Text;

namespace ApiTools.Codegen.Codegen
{
    public class ClientLibraryCodegen : CodegenBase
    {
        public ClientLibraryCodegen(LibraryBuilder libraryBuilder) : base(libraryBuilder)
        {
        }

        public override IEnumerable<BuildFile> Compile(BuildPackage package)
        {
            var sb = new StringBuilder();
            foreach (var type in libraryBuilder.ClientTypes)
            {
                sb.AppendLine($"namespace {package.Namespace}");
                sb.AppendLine("{");
                sb.AppendLine($"    public class {type.Name} : ClientObject");
                sb.AppendLine("    {");
                sb.AppendLine();
                sb.AppendLine("    }");
                sb.AppendLine("}");

                yield return new BuildFile
                {
                    Path = type.Name + ".cs",
                    Content = sb.ToString()
                };

                sb.Clear();
            }

            yield break;
        }
    }
}
