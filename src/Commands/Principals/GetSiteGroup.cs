using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteGroup", DefaultParameterSetName = "All")]
    [OutputType(typeof(SiteGroup))]
    public class GetSiteGroup : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = false)]
        public string Group;


        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if (ParameterSpecified(nameof(Site)))
            {
                url = Site.Url;
            }
            var site = this.Tenant.GetSiteByUrl(url);
            if (!ParameterSpecified(nameof(Group)))
            {
                var groups = AdminContext.LoadQuery(site.RootWeb.SiteGroups.IncludeWithDefaultProperties(g => g.Users, g => g.Title, g => g.OwnerTitle, g => g.Owner.LoginName, g => g.LoginName));
                AdminContext.ExecuteQueryRetry();
                foreach (var group in groups)
                {
                    var siteGroup = new SiteGroup(AdminContext, Tenant, group, site.RootWeb);
                    WriteObject(siteGroup);
                }
            }
            else
            {
                var group = site.RootWeb.SiteGroups.GetByName(Group);
                AdminContext.Load(group, g => g.Users, g => g.Title, g => g.OwnerTitle, g => g.Owner.LoginName, g => g.LoginName);
                AdminContext.ExecuteQueryRetry();
                var siteGroup = new SiteGroup(AdminContext, Tenant, group, site.RootWeb);
                WriteObject(siteGroup);
            }
        }
    }



}
