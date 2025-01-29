using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;

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
            var externalConnectionId = ConnectionId.GetExternalConnectionId(GraphRequestHelper) ?? throw new PSArgumentException("No valid external connection specified", nameof(ConnectionId));

            try
            {
                var response = GraphRequestHelper.Delete($"beta/external/connections/{externalConnectionId}/items/{ItemId}");
                WriteVerbose($"External item with ID '{ItemId}' successfully removed from external connection '{externalConnectionId}'");
            }
            catch (PSInvalidOperationException ex)
            {
                throw new PSInvalidOperationException($"Removing external item with ID '{ItemId}' from external connection '{externalConnectionId}' failed with message '{ex.Message}'", ex);
            }
        }
    }
}