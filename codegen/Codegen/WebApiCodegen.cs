using System.Collections.Generic;

namespace ApiTools.Codegen.Codegen
{
    public class WebApiCodegen : CodegenBase
    {
        public WebApiCodegen(LibraryBuilder libraryBuilder) : base(libraryBuilder)
        {
        }

        public override IEnumerable<BuildFile> Compile(BuildPackage package)
        {
            // web api shouldn't need much info about the types, if any
            yield break;
        }
    }   
}
