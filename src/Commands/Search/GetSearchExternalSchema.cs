using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Get, "PnPSearchExternalSchema")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/ExternalConnection.ReadWrite.OwnedBy")]
    [OutputType(typeof(Model.Graph.MicrosoftSearch.ExternalSchema))]
    public class GetSearchExternalSchema : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SearchExternalConnectionPipeBind ConnectionId;

        protected override void ExecuteCmdlet()
        {
            var searchExternalConnection = ConnectionId.GetExternalConnection(this, Connection, AccessToken);
            var graphApiUrl = $"v1.0/external/connections/{searchExternalConnection.Id}/schema";
            var result = Utilities.REST.GraphHelper.Get<Model.Graph.MicrosoftSearch.ExternalSchema>(this, Connection, graphApiUrl, AccessToken, additionalHeaders: new System.Collections.Generic.Dictionary<string, string> { { "Prefer", "include-unknown-enum-members" } });
            WriteObject(result, false);
        }
    }
}