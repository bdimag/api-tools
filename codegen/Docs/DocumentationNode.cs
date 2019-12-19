using System.Collections.Generic;

namespace ApiTools.Codegen.Docs
{
    public class DocumentationNode
    {
        private readonly Dictionary<string, DocumentationBody> memberParams = new Dictionary<string, DocumentationBody>();

        public IReadOnlyDictionary<string, DocumentationBody> Params
        {
            get
            {
                return memberParams;
            }
        }

        public DocumentationBody Returns { get; set; }

        public DocumentationBody Summary { get; set; }

        public void AddParam(string name, DocumentationBody body)
        {
            memberParams.Add(name, body);
        }
    }
}