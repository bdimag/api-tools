using Microsoft.Build.Framework;
using System.Runtime.CompilerServices;

namespace ApiTools.Codegen.Task
{
    public partial class CreateClientsFromAssembly : ITask
    {
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }


    }
}