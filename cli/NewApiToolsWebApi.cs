using System.Management.Automation;
using static ApiTools.Codegen.Cli.Res;

namespace ApiTools.Codegen.Cli
{
    public static class Res
    {
        internal const string ToolPrefix = "ApiTools";
    }

    [Cmdlet(VerbsCommon.New, ToolPrefix + "WebApi")]
    public class NewApiToolsWebApi : PSCmdlet
    {
        
    
    }
}