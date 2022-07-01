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
                var groups = ClientContext.LoadQuery(site.RootWeb.SiteGroups.IncludeWithDefaultProperties(g => g.Users, g => g.Title, g => g.OwnerTitle, g => g.Owner.LoginName, g => g.LoginName));
                ClientContext.ExecuteQueryRetry();
                foreach (var group in groups)
                {
                    var siteGroup = new SiteGroup(ClientContext, Tenant, group, site.RootWeb);
                    WriteObject(siteGroup);
                }
            }
            else
            {
                var group = site.RootWeb.SiteGroups.GetByName(Group);
                ClientContext.Load(group, g => g.Users, g => g.Title, g => g.OwnerTitle, g => g.Owner.LoginName, g => g.LoginName);
                ClientContext.ExecuteQueryRetry();
                var siteGroup = new SiteGroup(ClientContext, Tenant, group, site.RootWeb);
                WriteObject(siteGroup);
            }
        }
    }



}
