using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Remove, "PnPSearchExternalConnection")]
    [RequiredApiApplicationPermissions("graph/ExternalConnection.ReadWrite.OwnedBy")]
    public class RemoveSearchExternalConnection : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SearchExternalConnectionPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var externalConnection = Identity.GetExternalConnection(this, Connection, AccessToken);
            Utilities.REST.GraphHelper.Delete(this, Connection, $"v1.0/external/connections/{externalConnection.Id}", AccessToken);
        }
    }
}