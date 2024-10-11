using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation.Remoting;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Remove, "PnPSearchExternalItem")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ExternalItem.ReadWrite.OwnedBy")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ExternalItem.ReadWrite.All")]
    [OutputType(typeof(Model.Graph.MicrosoftSearch.ExternalItem))]
    public class RemoveSearchExternalItem : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SearchExternalConnectionPipeBind ConnectionId;

        [Parameter(Mandatory = true)]
        public string ItemId;

        protected override void ExecuteCmdlet()
        {
            var connection = ConnectionId.GetExternalConnection(this, Connection, AccessToken);

            try
            {
                var response = GraphHelper.Delete(this, Connection, $"beta/external/connections/{connection.Id}/items/{ItemId}", AccessToken);
                WriteVerbose($"External item with ID '{ItemId}' successfully removed from external connection '{connection.Id}'");
            }
            catch (PSInvalidOperationException ex)
            {
                throw new PSInvalidOperationException($"Removing external item with ID '{ItemId}' from external connection '{connection.Id}' failed with message '{ex.Message}'", ex);
            }
        }
    }
}