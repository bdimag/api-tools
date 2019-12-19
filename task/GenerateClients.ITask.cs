using Microsoft.Build.Framework;

namespace ApiTools.Codegen.Task
{
    public partial class GenerateClients : ITask
    {
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }
    }
}