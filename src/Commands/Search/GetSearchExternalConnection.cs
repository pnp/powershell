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

            if(ParameterSpecified(nameof(Identity)))
            {
                graphApiUrl += $"/{Identity}";

                WriteVerbose($"Retrieving external connection with Identity '{Identity}'");

                var externalConnectionResult = Utilities.REST.GraphHelper.Get<Model.Graph.MicrosoftSearch.ExternalConnection>(this, Connection, graphApiUrl, AccessToken);
                WriteObject(externalConnectionResult, false);
            }
            else
            {
                WriteVerbose("Retrieving all external connections");

                var externalConnectionResults = Utilities.REST.GraphHelper.GetResultCollection<Model.Graph.MicrosoftSearch.ExternalConnection>(this, Connection, graphApiUrl, AccessToken);
                WriteObject(externalConnectionResults, true);
            }
        }
    }
}