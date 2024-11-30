using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteSensitivityLabel")]
    [OutputType(typeof(void))]
    public class RemoveSiteSensitivityLabel : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteVerbose($"Verifying if the current site at {Connection.Url} is backed by a Microsoft 365 Group");
            ClientContext.Load(ClientContext.Site, s => s.GroupId);
            ClientContext.ExecuteQueryRetry();

            if(ClientContext.Site.GroupId != Guid.Empty)
            {
                // Site is Microsoft 365 Group backed
                WriteVerbose($"Current site at {Connection.Url} is backed by Microsoft 365 Group with Id {ClientContext.Site.GroupId}, going to try removing the label from the group");
                if(Connection.ConnectionMethod == Model.ConnectionMethod.AzureADAppOnly)
                {
                    WriteWarning("Current connection is not using a delegate token and is backed by a Microsoft 365 Group, removing the Microsoft Purview sensitivity label will likely not work. Check the documentation.");
                }

                WriteVerbose($"Trying to remove the Microsoft Purview sensitivity label from the Microsoft 365 Group with Id {ClientContext.Site.GroupId} behind the current site {Connection.Url}");
                var stringContent = new StringContent(JsonSerializer.Serialize(new { assignedLabels = new [] { new { labelId = "" }}}));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                RequestHelper.Patch(stringContent, $"beta/groups/{ClientContext.Site.GroupId}");
            }
            else
            {
                // Site does not have a Microsoft 365 Group behind it
                WriteVerbose($"Current site at {Connection.Url} is not backed by a Microsoft 365 Group");
            }

            WriteVerbose($"Trying to remove the Microsoft Purview sensitivity label from the current site {Connection.Url}");
            ClientContext.Site.SensitivityLabelId = null;
            ClientContext.ExecuteQueryRetry();
        }
    }
}