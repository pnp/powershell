using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPHubSite")]
    public class SetHubSite : PnPAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        [Alias("HubSite")]
        public HubSitePipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public string Title { get; set; }

        [Parameter(Mandatory = false)]
        public string LogoUrl { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public GuidPipeBind SiteDesignId;

        [Parameter(Mandatory = false)]
        public SwitchParameter HideNameInNavigation;

        [Parameter(Mandatory = false)]
        public SwitchParameter RequiresJoinApproval;

        protected override void ExecuteCmdlet()
        {
            var hubSiteProperties = Identity.GetHubSite(Tenant);
            ClientContext.Load(hubSiteProperties);
            if (ParameterSpecified(nameof(Title)))
            {
                hubSiteProperties.Title = Title;
            }
            if (ParameterSpecified(nameof(LogoUrl)))
            {
                hubSiteProperties.LogoUrl = LogoUrl;
            }
            if (ParameterSpecified(nameof(Description)))
            {
                hubSiteProperties.Description = Description;
            }
            if (ParameterSpecified(nameof(SiteDesignId)))
            {
                hubSiteProperties.SiteDesignId = SiteDesignId.Id;
            }
            if (ParameterSpecified(nameof(HideNameInNavigation)))
            {
                hubSiteProperties.HideNameInNavigation = HideNameInNavigation.ToBool();
            }
            if (ParameterSpecified(nameof(RequiresJoinApproval)))
            {
                hubSiteProperties.RequiresJoinApproval = RequiresJoinApproval.ToBool();
            }
            hubSiteProperties.Update();
            ClientContext.ExecuteQueryRetry();
        }
    }
}