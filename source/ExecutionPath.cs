namespace ApiTools
{
    public abstract class ExecutionPath
    {
        public ExecutionPath Parent { get; set; }

        public ExecutionPath(ExecutionPath parent)
        {
            Parent = parent;
        }
    }

    public class ExecutionPathStaticMethod : ExecutionPath
    {
        public string MethodName { get; }

        public object[] Parameters { get; }

        public string TypeId { get; }

        public ExecutionPathStaticMethod(string typeId, string methodName, object[] parameters) : base(null)
        {
            TypeId = typeId;
            MethodName = methodName;
            Parameters = parameters;
        }
    }
}