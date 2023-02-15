using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using Microsoft.Online.SharePoint.TenantManagement;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantSite")]
    public class GetTenantSite : PnPAdminCmdlet
    {
        private const string ParameterSet_BYURL = "By URL";
        private const string ParameterSet_ALL = "All Sites";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYURL)]
        [Alias("Url")]
        public SPOSitePipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public string Template;

        [Parameter(Mandatory = false)]
        public SwitchParameter Detailed;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public SwitchParameter IncludeOneDriveSites;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public string Filter;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYURL)]
        public SwitchParameter DisableSharingForNonOwnersStatus;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public bool? GroupIdDefined;

        protected override void ExecuteCmdlet()
        {
            ClientContext.ExecuteQueryRetry();
            if (ParameterSpecified(nameof(Identity)))
            {
                SiteProperties siteProperties;
                if(Identity.Id.HasValue)
                {
                    siteProperties = Tenant.GetSitePropertiesById(Identity.Id.Value, Detailed);
                    if(siteProperties == null) return;
                }
                else
                {
                    siteProperties = Tenant.GetSitePropertiesByUrl(Identity.Url, Detailed);
                    ClientContext.Load(siteProperties);
                    ClientContext.ExecuteQueryRetry();
                }
                Model.SPOSite site = null;
                if (ParameterSpecified(nameof(DisableSharingForNonOwnersStatus)))
                {
                    var office365Tenant = new Office365Tenant(ClientContext);
                    var clientResult = office365Tenant.IsSharingDisabledForNonOwnersOfSite(Identity.Url);
                    ClientContext.ExecuteQueryRetry();
                    site = new Model.SPOSite(siteProperties, clientResult.Value);
                }
                else
                {
                    site = new Model.SPOSite(siteProperties, null);
                }
                WriteObject(site, true);
            }
            else
            {
                SPOSitePropertiesEnumerableFilter filter = new SPOSitePropertiesEnumerableFilter()
                {
                    IncludePersonalSite = IncludeOneDriveSites.IsPresent ? PersonalSiteFilter.Include : PersonalSiteFilter.UseServerDefault,
                    IncludeDetail = Detailed,
                    Template = Template,
                    Filter = Filter,
                };

                if (ClientContext.ServerVersion >= new Version(16, 0, 7708, 1200))
                {
                    if (ParameterSpecified(nameof(GroupIdDefined)))
                    {
                        filter.GroupIdDefined = GroupIdDefined.Value == true ? 1 : 2;
                    }
                }
                else if (ParameterSpecified(nameof(GroupIdDefined)))
                {
                    throw new PSArgumentException("Filtering by Group Id is not yet available for this tenant.");
                }

                SPOSitePropertiesEnumerable sitesList = null;
                var sites = new List<SiteProperties>();
                do
                {
                    sitesList = Tenant.GetSitePropertiesFromSharePointByFilters(filter);
                    Tenant.Context.Load(sitesList);
                    Tenant.Context.ExecuteQueryRetry();
                    sites.AddRange(sitesList.ToList());
                    filter.StartIndex = sitesList.NextStartIndexFromSharePoint;

                } while (!string.IsNullOrWhiteSpace(sitesList.NextStartIndexFromSharePoint));

                if (Template != null)
                {
                    WriteObject(sites.Where(t => t.Template.ToLower() == Template.ToLower()).OrderBy(x => x.Url).Select(s => new Model.SPOSite(s, null)), true);
                }
                else
                {
                    WriteObject(sites.OrderBy(x => x.Url).Select(s => new Model.SPOSite(s, null)), true);
                }
            }
        }
    }
}