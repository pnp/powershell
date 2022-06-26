using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
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
            ClientContext.Load(ClientContext.Site, s => s.GroupId);
            ClientContext.ExecuteQueryRetry();

            if(ClientContext.Site.GroupId != Guid.Empty)
            {
                // Site is Microsoft 365 Group backed
                var stringContent = new StringContent(JsonSerializer.Serialize(new { assignedLabels = new [] { new { labelId = Identity }}}));
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                GraphHelper.PatchAsync(Connection, GraphAccessToken, stringContent, $"beta/groups/{ClientContext.Site.GroupId}").GetAwaiter().GetResult();;
            }
            else
            {
                // Site does not have a Microsoft 365 Group behind it

            }
        }
    }
}