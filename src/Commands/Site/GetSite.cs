using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using System;
using System.Linq.Expressions;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSite")]
    [OutputType(typeof(Microsoft.SharePoint.Client.Site))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Selected")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.ReadWrite.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Manage.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Write")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Manage")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
    public class GetSite : PnPRetrievalsCmdlet<Microsoft.SharePoint.Client.Site>
    {
        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Microsoft.SharePoint.Client.Site, object>>[] { s => s.Url, s => s.CompatibilityLevel };
            var site = ClientContext.Site;
            ClientContext.Load(site, RetrievalExpressions);
            ClientContext.ExecuteQueryRetry();
            WriteObject(site);
        }
    }
}
