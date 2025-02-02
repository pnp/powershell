using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteScript")]
    public class GetSiteScript : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public TenantSiteScriptPipeBind Identity;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public TenantSiteDesignPipeBind SiteDesign;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var scripts = Identity.GetTenantSiteScript(Tenant);

                foreach (var script in scripts)
                {
                    script.EnsureProperties(s => s.Content, s => s.Title, s => s.Id, s => s.Version, s => s.Description, s => s.IsSiteScriptPackage);
                }
                WriteObject(scripts, true);
            }
            else if (ParameterSpecified(nameof(SiteDesign)))
            {
                var scripts = new List<TenantSiteScript>();
                var design = Tenant.GetSiteDesign(AdminContext, SiteDesign.Id);
                design.EnsureProperty(d => d.SiteScriptIds);
                foreach (var scriptId in design.SiteScriptIds)
                {
                    var script = Tenant.GetSiteScript(AdminContext, scriptId);
                    script.EnsureProperties(s => s.Content, s => s.Title, s => s.Id, s => s.Version, s => s.Description, s => s.IsSiteScriptPackage);
                    scripts.Add(script);
                }
                WriteObject(scripts, true);
            }
            else
            {
                var scripts = Tenant.GetSiteScripts();
                AdminContext.Load(scripts, s => s.Include(sc => sc.Id, sc => sc.Title, sc => sc.Version, sc => sc.Description, sc => sc.Content, sc => sc.IsSiteScriptPackage));
                AdminContext.ExecuteQueryRetry();
                WriteObject(scripts, true);
            }
        }
    }
}