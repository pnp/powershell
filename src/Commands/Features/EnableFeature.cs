using System.Management.Automation;
using System;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Features
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPFeature")]
    [OutputType(typeof(void))]
    public class EnableFeature : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public Guid Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public FeatureScope Scope = FeatureScope.Web;

        protected override void ExecuteCmdlet()
        {
            var pnpContext = Connection.PnPContext;
            if (Scope == FeatureScope.Web)
            {
                pnpContext.Web.EnsureProperties(w => w.Features);
                pnpContext.Web.Features.Enable(Identity);
            }
            else
            {
                pnpContext.Site.EnsureProperties(s => s.Features);
                pnpContext.Site.Features.Enable(Identity);
            }
        }
    }
}
