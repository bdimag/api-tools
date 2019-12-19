namespace ApiTools.Codegen.Docs
{
    public class DocumentationTypedReference : DocumentationReference
    {
        public DocumentationReferenceType Type { get; }

        public DocumentationTypedReference(string value, DocumentationReferenceType type) : base(value)
        {
            Type = type;
        }
    }
}