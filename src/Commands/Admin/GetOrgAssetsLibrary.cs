using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPOrgAssetsLibrary")]
    public class GetOrgAssetsLibrary : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var results = Tenant.GetOrgAssets();
            AdminContext.ExecuteQueryRetry();

            List<OrgAssetsLibrary> orgassetlibs = results.Value?.OrgAssetsLibraries?.ToList();
            WriteObject(orgassetlibs, true);
        }
    }
}