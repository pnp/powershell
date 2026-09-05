using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPPersistedLogin")]
    [OutputType(typeof(TokenCacheConfiguration))]
    [ApiPermissionsNotRequired(Remarks = "This cmdlet reads the local persisted login configuration and performs no request.")]
    public class GetPersistedLogin : BasePSCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(PnPConnection.GetPersistedLoginEntries(), true);
        }
    }
}
