using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "SiteScript")]
    public class GetSiteScript : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public TenantSiteScriptPipeBind Identity;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public TenantSiteDesignPipeBind SiteDesign;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var script = Tenant.GetSiteScript(ClientContext, Identity.Id);
                script.EnsureProperties(s => s.Content, s => s.Title, s => s.Id, s => s.Version, s => s.Description);
                WriteObject(script);
            }
            else if (ParameterSpecified(nameof(SiteDesign)))
            {
                var scripts = new List<TenantSiteScript>();
                var design = Tenant.GetSiteDesign(ClientContext, SiteDesign.Id);
                design.EnsureProperty(d => d.SiteScriptIds);
                foreach (var scriptId in design.SiteScriptIds)
                {
                    var script = Tenant.GetSiteScript(ClientContext, scriptId);
                    script.EnsureProperties(s => s.Content, s => s.Title, s => s.Id, s => s.Version, s => s.Description);
                    scripts.Add(script);
                }
                WriteObject(scripts, true);
            }
            else
            {
                var scripts = Tenant.GetSiteScripts();
                ClientContext.Load(scripts, s => s.Include(sc => sc.Id, sc => sc.Title, sc => sc.Version, sc => sc.Description, sc => sc.Content));
                ClientContext.ExecuteQueryRetry();
                WriteObject(scripts.ToList(), true);
            }
        }
    }
}