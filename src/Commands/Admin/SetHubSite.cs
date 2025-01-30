using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPHubSite")]
    public class SetHubSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public HubSitePipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public string Title { get; set; }

        [Parameter(Mandatory = false)]
        public string LogoUrl { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public Guid SiteDesignId;

        [Parameter(Mandatory = false)]
        public SwitchParameter HideNameInNavigation;

        [Parameter(Mandatory = false)]
        public SwitchParameter RequiresJoinApproval;

        [Parameter(Mandatory = false)]
        public SwitchParameter EnablePermissionsSync;

        [Parameter(Mandatory = false)]
        public Guid ParentHubSiteId;

        protected override void ExecuteCmdlet()
        {
            var hubSiteProperties = Identity.GetHubSite(Tenant);
            AdminContext.Load(hubSiteProperties);
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
                hubSiteProperties.SiteDesignId = SiteDesignId;
            }
            if (ParameterSpecified(nameof(HideNameInNavigation)))
            {
                hubSiteProperties.HideNameInNavigation = HideNameInNavigation.ToBool();
            }
            if (ParameterSpecified(nameof(RequiresJoinApproval)))
            {
                hubSiteProperties.RequiresJoinApproval = RequiresJoinApproval.ToBool();
            }
            if (ParameterSpecified(nameof(EnablePermissionsSync)))
            {
                hubSiteProperties.EnablePermissionsSync = EnablePermissionsSync.ToBool();                
            }
            if (ParameterSpecified(nameof(ParentHubSiteId)))
            {
                hubSiteProperties.ParentHubSiteId = ParentHubSiteId;
            }
            hubSiteProperties.Update();
            AdminContext.ExecuteQueryRetry();
        }
    }
}