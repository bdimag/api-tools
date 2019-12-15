using System.Collections.Generic;

namespace ApiTools.Codegen.Codegen
{
    public abstract class CodegenBase
    {
        protected readonly LibraryBuilder libraryBuilder;

        internal CodegenBase(LibraryBuilder libraryBuilder)
        {
            this.libraryBuilder = libraryBuilder;
        }

        public abstract IEnumerable<BuildFile> Compile(BuildPackage package);
    }
}
