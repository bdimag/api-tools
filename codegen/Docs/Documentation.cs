using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ApiTools.Codegen.Docs
{
    public class Documentation
    {
        private readonly Dictionary<string, DocumentationNode> nodes = new Dictionary<string, DocumentationNode>();

        private static DocumentationBody CreateDocumentationBody(XElement element)
        {
            if (element is null || string.IsNullOrWhiteSpace(element.Value))
            {
                return null;
            }

            var newElement = new XElement(element.Name);
            var references = new List<DocumentationReference>();

            foreach (var node in element.Nodes())
            {
                // copy nodes;
                // update strings to escape '{' and '}' (becoming "{{" and "}}" respectively)
                // replace element with text and create a documentation reference

                if (node is XElement elementNode)
                {
                    var name = elementNode.Name.LocalName;
                    var preventDefault = false;

                    // TODO c, code, inheritdoc, list, para

                    if (elementNode.HasAttributes)
                    {
                        var attribute = elementNode.Attributes().First();
                        var attributeName = attribute.Name.LocalName;
                        var attributeValue = attribute.Value;

                        switch (name.ToUpperInvariant())
                        {
                            case "SEE":
                            case "SEEALSO":
                                var crefType = DocumentationReferenceType.NotSpecified;
                                var crefValue = string.Empty;
                                switch (attributeName.ToUpperInvariant())
                                {
                                    case "LANGWORD":
                                        crefType = DocumentationReferenceType.Langword;
                                        crefValue = attributeValue;
                                        break;

                                    case "CREF":

                                        var crefValues = attributeValue.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                                        if (crefValues.Length == 1)
                                        {
                                            crefType = DocumentationReferenceType.Type;
                                            crefValue = crefValues[0];
                                        }
                                        else
                                        {
                                            crefValue = crefValues[1];
                                            switch (crefValues[0].ToUpperInvariant())
                                            {
                                                case "T":
                                                    crefType = DocumentationReferenceType.Type;
                                                    break;

                                                case "F":
                                                    crefType = DocumentationReferenceType.Field;
                                                    break;

                                                case "P":
                                                    crefType = DocumentationReferenceType.Property;
                                                    break;

                                                case "M":
                                                    crefType = DocumentationReferenceType.Method;
                                                    break;

                                                case "E":
                                                    crefType = DocumentationReferenceType.Event;
                                                    break;

                                                case "!":
                                                    crefType = DocumentationReferenceType.Error;
                                                    break;
                                            }
                                        }

                                        break;
                                }

                                preventDefault = true;
                                references.Add(new DocumentationTypedReference(crefValue, crefType));
                                break;

                            case "PARAMREF":
                                preventDefault = true;
                                references.Add(new DocumentationTypedReference(attributeValue, DocumentationReferenceType.Parameter));
                                break;
                        }
                    }

                    if (preventDefault)
                    {
                        newElement.Add(new XText("{" + (references.Count - 1) + "}"));
                    }
                    else
                    {
                        newElement.Add(new XText(elementNode.ToString()));
                    }
                }
                else if (node is XText textNode)
                {
                    var text = new StringBuilder(textNode.Value);
                    text.Replace("{", "{{");
                    text.Replace("}", "}}");

                    newElement.Add(new XText(text.ToString()));
                }
            }

            return new DocumentationBody(newElement.Value.Trim(), references);
        }

        public static Documentation LoadFromXml(string contents)
        {
            throw new NotImplementedException();
        }

        public static Documentation LoadFromXml(Stream stream)
        {
            var documentation = new Documentation();
            using (var xmlReader = XmlReader.Create(new StreamReader(stream)))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
                    {
                        var element = XElement.Parse(xmlReader.ReadOuterXml());
                        var memberName = element.Attribute("name").Value;
                        var documentationNode = documentation.AddNode(memberName);

                        var innerText = new StringBuilder();
                        foreach (var node in element.Nodes())
                        {
                            if (node is XElement elementNode)
                            {
                                var name = elementNode.Name.LocalName;
                                var nodeBody = CreateDocumentationBody(elementNode);
                                switch (element.Name.LocalName.ToUpperInvariant())
                                {
                                    case "SUMMARY":
                                        documentationNode.Summary = nodeBody;
                                        break;

                                    case "PARAM":
                                        documentationNode.AddParam(name, nodeBody);
                                        break;

                                    case "RETURNS":
                                        documentationNode.Returns = nodeBody;
                                        break;

                                    default:
                                        break;
                                }
                            }
                            else if (node is XText textNode)
                            {
                                innerText.AppendLine(textNode.Value);
                            }
                        }

                        if (documentationNode.Summary == null && innerText.Length > 0)
                        {
                            documentationNode.Summary = new DocumentationBody(innerText.ToString());
                        }
                    }
                }
            }

            return documentation;
        }

        public DocumentationNode AddNode(string name)
        {
            var node = new DocumentationNode();
            nodes.Add(name, node);

            return node;
        }
    }
}