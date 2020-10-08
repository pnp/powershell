using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "HubToHubAssociation")]
    public class AddHubToHubAssociation : PnPAdminCmdlet
    {
        const string ParamSet_ById = "By Id";
        const string ParamSet_ByUrl = "By Url";

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ById)]
        [ValidateNotNull]
        public Guid Source;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ById)]
        [ValidateNotNull]
        public Guid Target;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUrl)]
        [ValidateNotNullOrEmpty]
        public string SourceUrl;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUrl)]
        [ValidateNotNullOrEmpty]
        public string TargetUrl;
        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParamSet_ById)
            {
                HubSiteProperties sourceProperties = this.Tenant.GetHubSitePropertiesById(Source);
                ClientContext.Load(sourceProperties);
                sourceProperties.ParentHubSiteId = Target;
                sourceProperties.Update();
                ClientContext.ExecuteQueryRetry();
            }
            else
            {
                HubSiteProperties sourceProperties = this.Tenant.GetHubSitePropertiesByUrl(SourceUrl);
                ClientContext.Load(sourceProperties);
                var targetSite = this.Tenant.GetSiteByUrl(TargetUrl);
                ClientContext.Load(targetSite);
                ClientContext.ExecuteQueryRetry();
                sourceProperties.ParentHubSiteId = targetSite.HubSiteId;
                sourceProperties.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}