using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Get, "PnPSearchExternalConnection")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ExternalConnection.ReadWrite.OwnedBy")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ExternalConnection.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ExternalConnection.ReadWrite.OwnedBy")]
    [OutputType(typeof(IEnumerable<Model.Graph.MicrosoftSearch.ExternalConnection>))]
    [OutputType(typeof(Model.Graph.MicrosoftSearch.ExternalConnection))]
    public class GetSearchExternalConnection : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            var graphApiUrl = $"v1.0/external/connections";

            if (ParameterSpecified(nameof(Identity)))
            {
                graphApiUrl += $"/{Identity}";

                LogDebug($"Retrieving external connection with Identity '{Identity}'");

                var externalConnectionResult = GraphRequestHelper.Get<Model.Graph.MicrosoftSearch.ExternalConnection>(graphApiUrl);
                WriteObject(externalConnectionResult, false);
            }
            else
            {
                LogDebug("Retrieving all external connections");

                var externalConnectionResults = GraphRequestHelper.GetResultCollection<Model.Graph.MicrosoftSearch.ExternalConnection>(graphApiUrl);
                WriteObject(externalConnectionResults, true);
            }
        }
    }
}