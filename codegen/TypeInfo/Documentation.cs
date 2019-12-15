using System.Collections.Generic;

namespace ApiTools.Codegen
{
    public class Documentation
    {
        public Dictionary<string, string> Params { get; set; }

        public string Returns { get; set; }

        public string Summary { get; set; }
    }
}