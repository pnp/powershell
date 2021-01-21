using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using System.Collections.Generic;
using Microsoft.Online.SharePoint.TenantManagement;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantSite")]
    public class GetTenantSite : PnPAdminCmdlet
    {
        private const string ParameterSet_BYURL = "By URL";
        private const string ParameterSet_ALL = "All Sites";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYURL)]
        public string Url;

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

        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Url))
            {
                var siteProperties = Tenant.GetSitePropertiesByUrl(Url, Detailed);
                ClientContext.Load(siteProperties);
                ClientContext.ExecuteQueryRetry();
                Model.SPOSite site = null;
                if (ParameterSpecified(nameof(DisableSharingForNonOwnersStatus)))
                {
                    var office365Tenant = new Office365Tenant(ClientContext);
                    var clientResult = office365Tenant.IsSharingDisabledForNonOwnersOfSite(Url);
                    ClientContext.ExecuteQuery();
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
#pragma warning disable CS0618 // Type or member is obsolete
                    Template = Template,
#pragma warning restore CS0618 // Type or member is obsolete
                    Filter = Filter,
                };
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
                    WriteObject(sites.Where(t => t.Template == Template).OrderBy(x => x.Url), true);
                }
                else
                {
                    WriteObject(sites.OrderBy(x => x.Url), true);
                }
            }
        }
    }
}