using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteScript")]
    public class SetSiteScript : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public TenantSiteScriptPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Content;

        [Parameter(Mandatory = false)]
        public int Version;

        protected override void ExecuteCmdlet()
        {
            var script = Tenant.GetSiteScript(AdminContext, Identity.Id);
            script.EnsureProperties(s => s.Content, s => s.Title, s => s.Id, s => s.Version, s => s.Description);
            if (script != null)
            {
                var isDirty = false;

                if (ParameterSpecified(nameof(Title)))
                {
                    script.Title = Title;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(Description)))
                {
                    script.Description = Description;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(Content)))
                {
                    script.Content = Content;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(Version)))
                {
                    script.Version = Version;
                    isDirty = true;
                }
                if (isDirty)
                {
                    Tenant.UpdateSiteScript(script);
                    AdminContext.ExecuteQueryRetry();
                    WriteObject(script);
                }
            }
        }
    }
}