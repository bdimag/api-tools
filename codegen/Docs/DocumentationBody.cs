using System;
using System.Collections.Generic;

namespace ApiTools.Codegen.Docs
{
    public class DocumentationBody
    {
        private readonly string innerText;

        private readonly List<DocumentationReference> references;

        public DocumentationBody(string innerText) : this(innerText, Array.Empty<DocumentationReference>())
        {
        }

        public DocumentationBody(string innerText, IEnumerable<DocumentationReference> references)
        {
            this.innerText = innerText;
            this.references = new List<DocumentationReference>(references);
        }

        public override string ToString()
        {
            return innerText;
        }
    }
}