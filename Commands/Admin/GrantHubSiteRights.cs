using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsSecurity.Grant, "PnPHubSiteRights")]
    [CmdletHelp(@"Grant additional permissions to the permissions already in place to associate sites to Hub Sites for one or more specific users",
        Category = CmdletHelpCategory.TenantAdmin)]
    [CmdletExample(Code = @"PS:> Grant-PnPHubSiteRights -Identity https://contoso.sharepoint.com/sites/hubsite -Principals ""myuser@mydomain.com"",""myotheruser@mydomain.com""", Remarks = "This example shows how to grant rights to myuser and myotheruser to associate their sites with the provided Hub Site", SortOrder = 1)]
    public class GrantHubSiteRights : PnPAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, HelpMessage = "The Hub Site to set the permissions on to associate another site with this Hub Site")]
        [Alias("HubSite")]
        public HubSitePipeBind Identity { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "One or more usernames that will be given or revoked the permission to associate a site with this Hub Site. It does not replace permissions given out before but adds to the already existing permissions.")]
        public string[] Principals { get; set; }

        protected override void ExecuteCmdlet()
        {
            base.Tenant.GrantHubSiteRights(Identity.Url ?? Identity.GetHubSite(Tenant).SiteUrl, Principals, SPOHubSiteUserRights.Join);
            ClientContext.ExecuteQueryRetry();
        }
    }
}