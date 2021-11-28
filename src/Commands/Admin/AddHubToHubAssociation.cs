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
                ClientObjectList<HubSiteProperties> hubSitesProperties = Tenant.GetHubSitesProperties();
                ClientContext.Load(hubSitesProperties);
                ClientContext.ExecuteQueryRetry();

                var sourceEnumerator = hubSitesProperties.GetEnumerator();
                var destEnumerator = hubSitesProperties.GetEnumerator();
                bool sourceHubSiteFound = false;
                bool destinationHubSiteFound = false;
                while (sourceEnumerator.MoveNext())
                {
                    HubSiteProperties hubsite = sourceEnumerator.Current;
                    if (hubsite.SiteUrl.ToLower() == SourceUrl.ToLower())
                    {
                        sourceHubSiteFound = true;
                        break;
                    }
                }

                while (destEnumerator.MoveNext())
                {
                    HubSiteProperties hubsite = destEnumerator.Current;
                    if (hubsite.SiteUrl.ToLower() == TargetUrl.ToLower())
                    {
                        destinationHubSiteFound = true;
                        break;
                    }
                }

                if (sourceHubSiteFound && destinationHubSiteFound)
                {
                    HubSiteProperties sourceProperties = Tenant.GetHubSitePropertiesByUrl(SourceUrl);
                    ClientContext.Load(sourceProperties);
                    Microsoft.SharePoint.Client.Site targetSite = Tenant.GetSiteByUrl(TargetUrl);
                    ClientContext.Load(targetSite);
                    ClientContext.ExecuteQueryRetry();
                    sourceProperties.ParentHubSiteId = targetSite.HubSiteId;
                    sourceProperties.Update();
                    ClientContext.ExecuteQueryRetry();
                }
                else
                {
                    throw new PSInvalidOperationException("Both source and destination site needs to be Hub sites.");
                }
            }
        }
    }
}