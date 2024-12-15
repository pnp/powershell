using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Get, "PnPSearchExternalSchema")]
    [RequiredApiApplicationPermissions("graph/ExternalConnection.ReadWrite.OwnedBy")]
    [OutputType(typeof(Model.Graph.MicrosoftSearch.ExternalSchema))]
    public class GetSearchExternalSchema : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SearchExternalConnectionPipeBind ConnectionId;

        protected override void ExecuteCmdlet()
        {
            var externalConnectionId = ConnectionId.GetExternalConnectionId(RequestHelper) ?? throw new PSArgumentException("No valid external connection specified", nameof(ConnectionId));
            var graphApiUrl = $"v1.0/external/connections/{externalConnectionId}/schema";
            var result = RequestHelper.Get<Model.Graph.MicrosoftSearch.ExternalSchema>(graphApiUrl, additionalHeaders: new System.Collections.Generic.Dictionary<string, string> { { "Prefer", "include-unknown-enum-members" } });
            WriteObject(result, false);
        }
    }
}