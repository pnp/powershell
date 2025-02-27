using System.Collections.Generic;
using System.Management.Automation;
using System.Text.Json;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Model.ServiceHealth;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365Roadmap")]
    [OutputType(typeof(List<Microsoft365RoadmapItem>))]
    public class GetMicrosoft365Roadmap : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string RoadmapUrl = "https://www.microsoft.com/releasecommunications/api/v1/m365";

        protected override void ProcessRecord()
        {
            WriteVerbose($"Retrieving the Microsoft 365 Roadmap from {RoadmapUrl}");

            var response = Framework.Http.PnPHttpClient.Instance.GetHttpClient().GetAsync(RoadmapUrl).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new PSInvalidOperationException($"Failed to retrieve the Microsoft 365 Roadmap from {RoadmapUrl}");
            }

            WriteVerbose("Successfully retrieved the Microsoft 365 Roadmap. Parsing roadmap content.");

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var roadmapItems = JsonSerializer.Deserialize<List<Microsoft365RoadmapItem>>(content, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            WriteVerbose($"{roadmapItems.Count} item{(roadmapItems.Count != 1 ? "s" : "")} parsed");

            WriteObject(roadmapItems, true);
        }
    }
}
