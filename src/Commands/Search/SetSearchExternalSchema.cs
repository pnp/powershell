using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Set, "PnPSearchExternalSchema")]
    [RequiredApiApplicationPermissions("graph/ExternalConnection.ReadWrite.OwnedBy")]
    [OutputType(typeof(string))]
    public class SetSearchExternalSchema : PnPGraphCmdlet
    {
        const string ParamSet_TextualSchema = "By textual schema";
        const string ParamSet_SchemaInstance = "By schema instance";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParamSet_TextualSchema)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParamSet_SchemaInstance)]
        public SearchExternalConnectionPipeBind ConnectionId;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_TextualSchema)]
        public string SchemaAsText;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_SchemaInstance)]
        public Model.Graph.MicrosoftSearch.ExternalSchema Schema;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_TextualSchema)]
        [Parameter(Mandatory = false, ParameterSetName = ParamSet_SchemaInstance)]
        public SwitchParameter Wait;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_TextualSchema)]
        [Parameter(Mandatory = false, ParameterSetName = ParamSet_SchemaInstance)]
        public short? OperationStatusPollingInterval = 30;

        protected override void ExecuteCmdlet()
        {
            var externalConnectionId = ConnectionId.GetExternalConnectionId(GraphRequestHelper) ?? throw new PSArgumentException("No valid external connection specified", nameof(ConnectionId));

            switch(ParameterSetName)
            {
                case ParamSet_TextualSchema:
                    LogDebug("Parsing schema from textual representation");
                    break;
                case ParamSet_SchemaInstance:
                    LogDebug("Using provided schema instance");
                    SchemaAsText = System.Text.Json.JsonSerializer.Serialize(Schema);
                    break;
            }

            var jsonContent = new StringContent(SchemaAsText);
            LogDebug($"Constructed payload: {jsonContent.ReadAsStringAsync().GetAwaiter().GetResult()}");

            var graphApiUrl = $"v1.0/external/connections/{externalConnectionId}/schema";
            var results = GraphRequestHelper.Patch(jsonContent, graphApiUrl);
            
            LogDebug("Trying to retrieve location header from response which can be used to poll for the status of the schema operation");
            if(results.Headers.TryGetValues("Location", out var location) && location.Any())
            {
                var schemaOperationStatusUrl = location.FirstOrDefault();
                LogDebug("Schema update has been scheduled");                

                if(Wait.ToBool())
                {
                    LogDebug($"Waiting for schema operation to complete by polling {schemaOperationStatusUrl}");

                    do
                    {
                        LogDebug("Polling schema operation status");
                        var schemaOperationResult = GraphRequestHelper.Get<Model.Graph.OperationStatus>(schemaOperationStatusUrl);

                        if(!string.IsNullOrEmpty(schemaOperationResult.Status))
                        {
                            if (schemaOperationResult.Status.ToLowerInvariant() == "completed")
                            {
                                LogDebug("Schema operation has completed");
                                break;
                            }
                            else
                            {
                                LogDebug($"Schema operation still in progress with status {schemaOperationResult.Status}");
                            }
                        }

                        LogDebug($"Waiting for {OperationStatusPollingInterval.GetValueOrDefault(30)} seconds before polling again");
                        Thread.Sleep(TimeSpan.FromSeconds(OperationStatusPollingInterval.GetValueOrDefault(30)));
                    }
                    while (true);
                }

                WriteObject(schemaOperationStatusUrl, false);
            }
            else
            {
                LogDebug("No valid Location header found in response");
            }
        }
    }
}