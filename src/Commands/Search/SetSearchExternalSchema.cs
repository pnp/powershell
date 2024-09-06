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
    [RequiredMinimalApiPermissions("ExternalConnection.ReadWrite.OwnedBy")]
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
            var searchExternalConnection = ConnectionId.GetExternalConnection(this, Connection, AccessToken);

            switch(ParameterSetName)
            {
                case ParamSet_TextualSchema:
                    WriteVerbose("Parsing schema from textual representation");
                    break;
                case ParamSet_SchemaInstance:
                    WriteVerbose("Using provided schema instance");
                    SchemaAsText = System.Text.Json.JsonSerializer.Serialize(Schema);
                    break;
            }

            var jsonContent = new StringContent(SchemaAsText);
            WriteVerbose($"Constructed payload: {jsonContent.ReadAsStringAsync().GetAwaiter().GetResult()}");

            var graphApiUrl = $"v1.0/external/connections/{searchExternalConnection.Id}/schema";
            var results = Utilities.REST.GraphHelper.Patch(this, Connection, AccessToken, jsonContent, graphApiUrl);
            
            WriteVerbose("Trying to retrieve location header from response which can be used to poll for the status of the schema operation");
            if(results.Headers.TryGetValues("Location", out var location) && location.Any())
            {
                var schemaOperationStatusUrl = location.FirstOrDefault();
                WriteVerbose("Schema update has been scheduled");                

                if(Wait.ToBool())
                {
                    WriteVerbose($"Waiting for schema operation to complete by polling {schemaOperationStatusUrl}");

                    do
                    {
                        WriteVerbose("Polling schema operation status");
                        var schemaOperationResult = Utilities.REST.GraphHelper.Get<Model.Graph.OperationStatus>(this, Connection, schemaOperationStatusUrl, AccessToken);

                        if(!string.IsNullOrEmpty(schemaOperationResult.Status))
                        {
                            if (schemaOperationResult.Status.ToLowerInvariant() == "completed")
                            {
                                WriteVerbose("Schema operation has completed");
                                break;
                            }
                            else
                            {
                                WriteVerbose($"Schema operation still in progress with status {schemaOperationResult.Status}");
                            }
                        }

                        WriteVerbose($"Waiting for {OperationStatusPollingInterval.GetValueOrDefault(30)} seconds before polling again");
                        Thread.Sleep(TimeSpan.FromSeconds(OperationStatusPollingInterval.GetValueOrDefault(30)));
                    }
                    while (true);
                }

                WriteObject(schemaOperationStatusUrl, false);
            }
            else
            {
                WriteVerbose("No valid Location header found in response");
            }
        }
    }
}