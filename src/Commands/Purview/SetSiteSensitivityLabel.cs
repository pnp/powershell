using Microsoft.SharePoint.Client;
using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteSensitivityLabel")]
    [OutputType(typeof(void))]
    public class SetSiteSensitivityLabel : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            string sensitivityLabelId;
            if (!Guid.TryParse(Identity, out Guid sensitivityLabelGuid))
            {
                // Look up the sensitivity label Guid with the provided name
                LogDebug($"Passed in label '{Identity}' is a name, going to try to lookup its Id");

                var label = GraphRequestHelper.GetResultCollection<Model.Graph.Purview.InformationProtectionLabel>($"/beta/{(Connection.ConnectionMethod == Model.ConnectionMethod.AzureADAppOnly ? "" : "me/")}informationProtection/policy/labels?$filter=name eq '{Identity}'");
                if (label == null || label.Count() == 0)
                {
                    throw new PSArgumentException($"No Microsoft Purview sensitivity label with the provided name '{Identity}' could be found", nameof(Identity));
                }

                sensitivityLabelId = label.ElementAt(0).Id?.ToString();
                LogDebug($"Microsoft Purview label with name '{Identity}' successfully resolved to Id '{sensitivityLabelId}'");
            }
            else
            {
                // Sensitivity label has been passed in by its Id, we can use it as provided
                LogDebug($"Passed in label '{Identity} is a Guid, going to use it as is");
                sensitivityLabelId = sensitivityLabelGuid.ToString();
            }

            LogDebug($"Verifying if the current site at {Connection.Url} is backed by a Microsoft 365 Group");
            ClientContext.Load(ClientContext.Site, s => s.GroupId);
            ClientContext.ExecuteQueryRetry();

            if (ClientContext.Site.GroupId != Guid.Empty)
            {
                // Site is Microsoft 365 Group backed
                LogDebug($"Current site at {Connection.Url} is backed by Microsoft 365 Group with Id {ClientContext.Site.GroupId}, going to try setting the label on the group");
                if (Connection.ConnectionMethod == Model.ConnectionMethod.AzureADAppOnly)
                {
                    LogWarning("Current connection is not using a delegate token and is backed by a Microsoft 365 Group, setting the Microsoft Purview sensitivity label will likely not work. Check the documentation.");
                }

                LogDebug($"Trying to set the Microsoft 365 Group with Id {ClientContext.Site.GroupId} behind the current site {Connection.Url} to Microsoft Purview sensitivity label with Id {sensitivityLabelId}");
                var stringContent = new StringContent(JsonSerializer.Serialize(new { assignedLabels = new[] { new { labelId = sensitivityLabelId } } }));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                GraphRequestHelper.Patch(stringContent, $"beta/groups/{ClientContext.Site.GroupId}");
            }
            else
            {
                // Site does not have a Microsoft 365 Group behind it
                LogDebug($"Current site at {Connection.Url} is not backed by a Microsoft 365 Group");
            }

            LogDebug($"Trying to set the current site {Connection.Url} to Microsoft Purview sensitivity label with Id {sensitivityLabelId}");
            ClientContext.Site.SensitivityLabelId = sensitivityLabelId;
            ClientContext.ExecuteQueryRetry();
        }
    }
}