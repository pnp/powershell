using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Linq;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Get, "PnPSearchExternalItem")]
    [RequiredApiDelegatedPermissions("graph/ExternalItem.Read.All")]
    [ApiNotAvailableUnderApplicationPermissions]
    [OutputType(typeof(Model.Graph.MicrosoftSearch.ExternalItem[]))]
    public class GetSearchExternalItem : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SearchExternalConnectionPipeBind ConnectionId;

        [Parameter(Mandatory = false)]
        [ValidateLength(1,128)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            var externalConnection = ConnectionId.GetExternalConnection(this, Connection, AccessToken);

            var searchQuery = new Model.Graph.MicrosoftSearch.SearchRequests
            {
                Requests =
                [
                    new ()
                    {
                        EntityTypes =
                        [
                            "externalItem"
                        ],
                        ContentSources =
                        [
                            $"/external/connections/{externalConnection.Id}"
                        ],
                        Query = new Model.Graph.MicrosoftSearch.SearchRequestQuery
                        {
                            QueryString = ParameterSpecified(nameof(Identity)) ? $"fileID:{Identity}" : "*"
                        }
                    } 
                ]
            };

            var httpContent = new StringContent(JsonSerializer.Serialize(searchQuery), System.Text.Encoding.UTF8);
            httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            // Execute the search query to discover the external items
            var searchResults = GraphHelper.Post<RestResultCollection<Model.Graph.MicrosoftSearch.SearchResult>>(this, Connection, "v1.0/search/query", httpContent, AccessToken);

            var hits = searchResults.Items.FirstOrDefault().HitsContainers.FirstOrDefault().Hits;

            if(hits == null || hits.Count == 0)
            {
                WriteVerbose($"No external items found{(ParameterSpecified(nameof(Identity)) ? $" with the identity '{Identity}'" : "")} on external connection '{externalConnection.Id}'");
                return;
            }

            WriteVerbose($"Found {hits.Count} external item{(hits.Count != 1 ? "s" : "")}{(ParameterSpecified(nameof(Identity)) ? $" with the identity '{Identity}'" : "")} on external connection '{externalConnection.Id}'");

            var externalItems = hits.Select(s => new Model.Graph.MicrosoftSearch.ExternalItem {  
                Id = s.Resource.Properties["fileID"].ToString()[(s.Resource.Properties["fileID"].ToString().LastIndexOf(',') + 1)..],
                Acls = null,
                Properties = new System.Collections.Hashtable(s.Resource.Properties),
                Content = new Model.Graph.MicrosoftSearch.ExternalItemContent { Type = Enums.SearchExternalItemContentType.Html, Value = s.Summary }
            }).ToArray();

            WriteObject(externalItems, true);
        }
    }
}