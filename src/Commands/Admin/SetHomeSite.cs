using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.PortalAndOrgNews;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPHomeSite")]
    public class SetHomeSite : PnPAdminCmdlet
    {
        [Alias("Url")]
        [Parameter(Mandatory = true)]
        public string HomeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter VivaConnectionsDefaultStart;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter DraftMode;

        protected override void ExecuteCmdlet()
        {
            Tenant.EnsureProperties(t => t.IsMultipleVivaConnectionsFlightEnabled, t => t.IsVivaHomeFlightEnabled);

            if (Tenant.IsMultipleVivaConnectionsFlightEnabled)
            {
                if (Force.IsPresent || ShouldContinue("Before you update a home site or Viva Connections experiences, make sure you review the documentation at https://aka.ms/homesites. Continue?", string.Empty))
                {
                    IEnumerable<TargetedSiteDetails> enumerable = Tenant.GetTargetedSitesDetails()?.Where((TargetedSiteDetails hs) => !hs.IsVivaBackendSite);
                    AdminContext.ExecuteQueryRetry();
                    bool flag = false;
                    if (enumerable == null || enumerable.Count() == 0)
                    {
                        Tenant.AddHomeSite(HomeSiteUrl, 1, null);
                        AdminContext.ExecuteQueryRetry();
                        flag = true;
                    }
                    else if (enumerable.Count() == 1 && !IsSameSiteUrl(enumerable.First().Url, HomeSiteUrl))
                    {
                        Tenant.RemoveTargetedSite(enumerable.First().SiteId);
                        AdminContext.ExecuteQueryRetry();
                        Tenant.AddHomeSite(HomeSiteUrl, 1, null);
                        AdminContext.ExecuteQuery();
                        flag = true;
                    }
                    HomeSiteConfigurationParam configurationParam = new()
                    {
                        vivaConnectionsDefaultStart = VivaConnectionsDefaultStart,
                        IsVivaConnectionsDefaultStartPresent = VivaConnectionsDefaultStart,
                        isInDraftMode = DraftMode,
                        IsInDraftModePresent = DraftMode || flag
                    };
                    ClientResult<TargetedSiteDetails> clientResult = Tenant.UpdateTargetedSite(HomeSiteUrl, configurationParam);
                    AdminContext.ExecuteQueryRetry();
                    WriteObject(clientResult.Value);
                }
            }
            else if (Force.IsPresent || ShouldContinue("Before you set a Home site, make sure you review the documentation at https://aka.ms/homesites. Continue?", string.Empty))
            {
                Tenant.ValidateVivaHomeParameterExists(VivaConnectionsDefaultStart);                
                HomeSiteConfigurationParam configuration = null;
                if (VivaConnectionsDefaultStart || DraftMode)
                {
                    configuration = new HomeSiteConfigurationParam
                    {
                        vivaConnectionsDefaultStart = VivaConnectionsDefaultStart,
                        IsVivaConnectionsDefaultStartPresent = VivaConnectionsDefaultStart,
                        isInDraftMode = DraftMode,
                        IsInDraftModePresent = DraftMode
                    };
                }
                ClientResult<string> clientResult = Tenant.SetSPHSiteWithConfiguration(HomeSiteUrl, configuration);
                AdminContext.ExecuteQueryRetry();
                WriteObject(clientResult.Value);
            }            
        }

        private static bool IsSameSiteUrl(string url1, string url2)
        {
            Uri uri = new(url1);
            Uri uri2 = new(url2);
            return Uri.Compare(uri, uri2, UriComponents.Host | UriComponents.Path, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}