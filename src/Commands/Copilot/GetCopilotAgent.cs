using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.Copilot;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Copilot
{
    [Cmdlet(VerbsCommon.Get, "PnPCopilotAgent")]
    [OutputType("PnP.PowerShell.Commands.Model.Copilot.CopilotAgent")]
    public class GetCopilotAgent : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public string ServerRelativeUrl;

        protected override void ExecuteCmdlet()
        {

            if (ParameterSpecified(nameof(ServerRelativeUrl)))
            {
                try
                {
                    var agentContents = CurrentWeb.GetFileAsString(ServerRelativeUrl);
                    var agentObject = JsonSerializer.Deserialize<CopilotAgent>(agentContents);
                    agentObject.ServerRelativeUrl = ServerRelativeUrl;
                    WriteObject(agentObject);
                }
                catch (JsonException)
                {
                    throw new PSNotSupportedException("Cannot extract agent information from contents.");
                }
                catch (ServerException)
                {
                    throw new PSArgumentException($"Agent with url {ServerRelativeUrl} not found.");
                }
            }
            else
            {
                // find all doclibraries
                var doclibs = ClientContext.LoadQuery(CurrentWeb.Lists.Where(l => l.BaseTemplate == (int)ListTemplateType.DocumentLibrary));
                ClientContext.ExecuteQueryRetry();

                foreach (var doclib in doclibs)
                {
                    GetAgents(doclib);
                }
            }
        }

        private void GetAgents(List list)
        {
            var camlQuery = CamlQuery.CreateAllItemsQuery(100);
            camlQuery.ViewXml = "<View Scope=\"RecursiveAll\"><Query><Where><Eq><FieldRef Name=\"File_x0020_Type\" /><Value Type=\"Text\">agent</Value></Eq></Where></Query></View>";

            // Initialize position to null for first page
            ListItemCollectionPosition position = null;

            // Continue fetching until no more pages
            do
            {
                // Set the position for the current request
                camlQuery.ListItemCollectionPosition = position;

                // Get current batch of items
                var items = list.GetItems(camlQuery);
                list.Context.Load(items, i => i.IncludeWithDefaultProperties(li => li.FieldValuesAsText), i => i.ListItemCollectionPosition);
                ClientContext.ExecuteQueryRetry();

                // Process current batch
                foreach (var item in items)
                {
                    var agentContents = CurrentWeb.GetFileAsString(item.FieldValuesAsText["FileRef"]);
                    var agentObject = JsonSerializer.Deserialize<CopilotAgent>(agentContents);
                    agentObject.ServerRelativeUrl = item.FieldValuesAsText["FileRef"];

                    WriteObject(agentObject);
                }

                // Get position for next batch
                position = items.ListItemCollectionPosition;

            } while (position != null);

        }
    }
}