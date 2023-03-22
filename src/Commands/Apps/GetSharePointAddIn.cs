using PnP.Core.Services;
using System.Collections.Generic;
using System.Management.Automation;
using PnP.Core.Admin.Model.SharePoint;
using System;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Base;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPSharePointAddIn")]
    [OutputType(typeof(List<ISharePointAddIn>))]
    public class GetSharePointAddIn : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSubsites;

        protected override void ExecuteCmdlet()
        {
            var tenantAdminSiteUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);

            VanityUrlOptions vanityUrlOptions = new()
            {
                AdminCenterUri = new Uri(tenantAdminSiteUrl)
            };

            using var context = ClientContext.Clone(Connection.Url);

            // need to retrieve PnPContext for the connected site not the admin site
            using var pnpContext = Framework.PnPCoreSdk.Instance.GetPnPContext(context);
            
            List<ISharePointAddIn> addIns = pnpContext.GetSiteCollectionManager().GetSiteCollectionSharePointAddIns(IncludeSubsites, vanityUrlOptions);
            WriteObject(addIns, true);
        }
    }
}
