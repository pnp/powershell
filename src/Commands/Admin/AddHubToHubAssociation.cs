using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
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
                ClientContext.Load(sourceProperties);
                sourceProperties.ParentHubSiteId = Target;
                sourceProperties.Update();
                ClientContext.ExecuteQueryRetry();
            }
            else
            {
                SiteProperties sourceSiteProperties = Tenant.GetSitePropertiesByUrl(SourceUrl, true);
                ClientContext.Load(sourceSiteProperties);
                ClientContext.ExecuteQueryRetry();

                SiteProperties destSiteProperties = Tenant.GetSitePropertiesByUrl(TargetUrl, true);
                ClientContext.Load(destSiteProperties);
                ClientContext.ExecuteQueryRetry();

                if (!sourceSiteProperties.IsHubSite)
                {
                    throw new PSInvalidOperationException("Source site collection needs to be a Hub site.");
                }

                if (!destSiteProperties.IsHubSite)
                {
                    throw new PSInvalidOperationException("Destination site collection needs to be a Hub site.");
                }
                
                HubSiteProperties sourceProperties = Tenant.GetHubSitePropertiesByUrl(SourceUrl);
                ClientContext.Load(sourceProperties);
                Microsoft.SharePoint.Client.Site targetSite = Tenant.GetSiteByUrl(TargetUrl);
                ClientContext.Load(targetSite);
                ClientContext.ExecuteQueryRetry();
                sourceProperties.ParentHubSiteId = targetSite.HubSiteId;
                sourceProperties.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}