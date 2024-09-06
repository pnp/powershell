using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using System.Net.Http.Json;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.New, "PnPSearchExternalConnection")]
    [RequiredMinimalApiPermissions("ExternalConnection.ReadWrite.OwnedBy")]
    [OutputType(typeof(Model.Graph.MicrosoftSearch.ExternalConnection))]
    public class NewSearchExternalConnection : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateLength(3, 32)]
        public string Identity;

        [Parameter(Mandatory = true)]
        [ValidateLength(1, 128)]
        public string Name;

        [Parameter(Mandatory = false)]
        public string Description;        

        [Parameter(Mandatory = false)]
        public string[] AuthorizedAppIds;
        protected override void ExecuteCmdlet()
        {
            var bodyContent = new Model.Graph.MicrosoftSearch.ExternalConnection
            {
                Id = Identity,
                Name = Name,
                Description = Description
            };

            if(ParameterSpecified(nameof(AuthorizedAppIds)))
            {
                bodyContent.Configuration = new() {
                    AuthorizedAppIds = AuthorizedAppIds
                };
            }

            var jsonContent = JsonContent.Create(bodyContent);
            WriteVerbose($"Constructed payload: {jsonContent.ReadAsStringAsync().GetAwaiter().GetResult()}");

            var graphApiUrl = $"v1.0/external/connections";
            var results = Utilities.REST.GraphHelper.Post(this, Connection, graphApiUrl, AccessToken, jsonContent);
            var resultsContent = results.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var externalConnectionResult = System.Text.Json.JsonSerializer.Deserialize<Model.Graph.MicrosoftSearch.ExternalConnection>(resultsContent);

            WriteObject(externalConnectionResult, false);
        }
    }
}