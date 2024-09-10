using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using System.Net.Http.Json;
using System.Text.Json;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Set, "PnPSearchExternalConnection")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/ExternalConnection.ReadWrite.OwnedBy")]
    public class SetSearchExternalConnection : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SearchExternalConnectionPipeBind Identity;

        [Parameter(Mandatory = false)]
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
                Name = Name,
                Description = Description
            };

            if(ParameterSpecified(nameof(AuthorizedAppIds)))
            {
                bodyContent.Configuration = new() {
                    AuthorizedAppIds = AuthorizedAppIds
                };
            }

            var jsonContent = JsonContent.Create(bodyContent, null, new JsonSerializerOptions { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });
            WriteVerbose($"Constructed payload: {jsonContent.ReadAsStringAsync().GetAwaiter().GetResult()}");

            var externalConnection = Identity.GetExternalConnection(this, Connection, AccessToken);
            var graphApiUrl = $"v1.0/external/connections/{externalConnection.Id}";
            Utilities.REST.GraphHelper.Patch(this, Connection, AccessToken, jsonContent, graphApiUrl);
        }
    }
}