using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Codegen
{
    public class ClientType : ClientMemberInfo
    {
        public ClientMethod DefaultConstructor
        {
            get
            {
                // TODO in the case of multiple constructors, we'd probably want the parameterless one
                return Constructors.FirstOrDefault(); 
            }
        }

        public IEnumerable<ClientMethod> Constructors { get; set; }

        public string Id { get; set; }

        public IEnumerable<ClientMethod> Methods { get; set; }

        public IEnumerable<ClientProperty> Properties { get; set; }
    }
}
