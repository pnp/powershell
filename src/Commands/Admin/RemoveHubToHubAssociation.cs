using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPHubToHubAssociation")]
    public class RemoveHubToHubAssociation : PnPAdminCmdlet
    {
        const string ParamSet_ById = "By Id";
        const string ParamSet_ByUrl = "By Url";

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ById)]
        [ValidateNotNull]
        public Guid HubSiteId;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUrl)]
        [ValidateNotNullOrEmpty]
        public string HubSiteUrl;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParamSet_ById)
            {
                HubSiteProperties sourceProperties = this.Tenant.GetHubSitePropertiesById(HubSiteId);
                AdminContext.Load(sourceProperties);
                sourceProperties.ParentHubSiteId = Guid.Empty;
                sourceProperties.Update();
                AdminContext.ExecuteQueryRetry();
            }
            else
            {
                HubSiteProperties sourceProperties = this.Tenant.GetHubSitePropertiesByUrl(HubSiteUrl);
                AdminContext.Load(sourceProperties);
                sourceProperties.ParentHubSiteId = Guid.Empty;
                sourceProperties.Update();
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}