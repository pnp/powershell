using System.Management.Automation;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.Copilot;
using System.Linq;
using System;

namespace PnP.PowerShell.Commands.Copilot
{
    [Cmdlet(VerbsCommon.Get, "PnPCopilotAgent")]
    [OutputType("PnP.PowerShell.Commands.Model.Copilot.CopilotAgent")]
    public class NewCopilotAgent : PnPWebCmdlet
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
                var doclibs = ClientContext.LoadQuery(CurrentWeb.Lists.Where(l => l.BaseTemplate == 101));
                ClientContext.ExecuteQuery();
                var camlQuery = CamlQuery.CreateAllItemsQuery();

                camlQuery.ViewXml = "<View Scope=\"RecursiveAll\"><Query><Where><Eq><FieldRef Name=\"File_x0020_Type\" /><Value Type=\"Text\">agent</Value></Eq></Where></Query></View>";
                foreach (var doclib in doclibs)
                {
                    camlQuery.ListItemCollectionPosition = null;
                    do
                    {
                        camlQuery.ListItemCollectionPosition = GetAgents(doclib, camlQuery);

                    } while (camlQuery.ListItemCollectionPosition != null);
                }
            }
        }

        private ListItemCollectionPosition GetAgents(List list, CamlQuery camlQuery)
        {
            var items = list.GetItems(camlQuery);
            list.Context.Load(items, i => i.IncludeWithDefaultProperties(li => li.FieldValuesAsText), i => i.ListItemCollectionPosition);
            ClientContext.ExecuteQueryRetry();
            foreach (var item in items)
            {
                var agentContents = CurrentWeb.GetFileAsString(item.FieldValuesAsText["FileRef"]);
                var agentObject = JsonSerializer.Deserialize<CopilotAgent>(agentContents);
                agentObject.ServerRelativeUrl = item.FieldValuesAsText["FileRef"];
                WriteObject(agentObject);
            }

            return items.ListItemCollectionPosition;
        }
    }
}