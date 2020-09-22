using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantSite")]
    public class GetTenantSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        [Alias("Identity")]
        public string Url;

        [Parameter(Mandatory = false)]
        public string Template;

        [Parameter(Mandatory = false)]
        public SwitchParameter Detailed;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeOneDriveSites;

        [Parameter(Mandatory = false)]
        public string Filter;

        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Url))
            {
                var list = Tenant.GetSitePropertiesByUrl(Url, Detailed);
                list.Context.Load(list);
                list.Context.ExecuteQueryRetry();
                WriteObject(list, true);
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