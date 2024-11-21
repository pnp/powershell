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
                pnpContext.Web.LoadAsync(w => w.Features).GetAwaiter().GetResult();
                pnpContext.Web.Features.EnableAsync(Identity).GetAwaiter().GetResult();
            }
            else
            {
                pnpContext.Site.LoadAsync(s => s.Features).GetAwaiter().GetResult();
                pnpContext.Site.Features.EnableAsync(Identity).GetAwaiter().GetResult();
            }
        }
    }
}
