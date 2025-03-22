using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSiteSwap")]
    public class InvokeSiteSwap : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory =true)]
        public string SourceUrl;

        [Parameter(Mandatory = true)]
        public string TargetUrl;

        [Parameter(Mandatory = true)]
        public string ArchiveUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter DisableRedirection;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWait;

        [Parameter(Position = 0, ValueFromPipeline = true)]
        public HubSitePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            var includeSmartGestures = !DisableRedirection;

            LogDebug($"Invoking site swap with source {SourceUrl}, target {TargetUrl} and archive {ArchiveUrl}");

            var operation = this.Tenant.SwapSiteWithSmartGestureOption(SourceUrl, TargetUrl, ArchiveUrl, includeSmartGestures);
            AdminContext.Load(operation);
            AdminContext.ExecuteQueryRetry();

            if(!ParameterSpecified(nameof(NoWait)))
            {
                PollOperation(operation);
            }
        }
    }
}