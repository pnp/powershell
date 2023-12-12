using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteGroup", SupportsShouldProcess = true)]
    [OutputType(typeof(void))]
    public class RemoveSiteGroup : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        public string Identity;


        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if (ParameterSpecified(nameof(Site)))
            {
                url = Site.Url;
            }
            var site = this.Tenant.GetSiteByUrl(url);
            if(ShouldProcess($"Deletes group {Identity} from the site {url}"))
            {
                var siteGroups = site.RootWeb.SiteGroups;
                siteGroups.RemoveByLoginName(Identity);
                site.RootWeb.Update();
                AdminContext.ExecuteQueryRetry();
            }
        }
    }



}
