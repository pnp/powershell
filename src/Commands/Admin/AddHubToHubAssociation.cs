using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPHubToHubAssociation")]
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
                HubSiteProperties sourceProperties = Tenant.GetHubSitePropertiesById(Source);
                AdminContext.Load(sourceProperties);
                sourceProperties.ParentHubSiteId = Target;
                sourceProperties.Update();
                AdminContext.ExecuteQueryRetry();
            }
            else
            {
                SiteProperties sourceSiteProperties = Tenant.GetSitePropertiesByUrl(SourceUrl, true);
                AdminContext.Load(sourceSiteProperties);
                AdminContext.ExecuteQueryRetry();

                SiteProperties destSiteProperties = Tenant.GetSitePropertiesByUrl(TargetUrl, true);
                AdminContext.Load(destSiteProperties);
                AdminContext.ExecuteQueryRetry();

                if (!sourceSiteProperties.IsHubSite)
                {
                    throw new PSInvalidOperationException("Source site collection needs to be a Hub site.");
                }

                if (!destSiteProperties.IsHubSite)
                {
                    throw new PSInvalidOperationException("Destination site collection needs to be a Hub site.");
                }
                
                HubSiteProperties sourceProperties = Tenant.GetHubSitePropertiesByUrl(SourceUrl);
                AdminContext.Load(sourceProperties);
                Microsoft.SharePoint.Client.Site targetSite = Tenant.GetSiteByUrl(TargetUrl);
                AdminContext.Load(targetSite);
                AdminContext.ExecuteQueryRetry();
                sourceProperties.ParentHubSiteId = targetSite.HubSiteId;
                sourceProperties.Update();
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}