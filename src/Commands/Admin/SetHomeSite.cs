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
    public class SetHomeSite : PnPSharePointOnlineAdminCmdlet
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
            HomeSiteConfigurationParam configurationParam;

            bool hasVivaConnectionsDefaultStart = ParameterSpecified(nameof(VivaConnectionsDefaultStart));
            bool hasDraftMode = ParameterSpecified(nameof(DraftMode));

            if (hasVivaConnectionsDefaultStart && hasDraftMode)
            {
                configurationParam = new()
                {
                    vivaConnectionsDefaultStart = VivaConnectionsDefaultStart,
                    IsVivaConnectionsDefaultStartPresent = true,
                    isInDraftMode = DraftMode,
                    IsInDraftModePresent = true
                };
            }
            else if (hasVivaConnectionsDefaultStart)
            {
                configurationParam = new()
                {
                    vivaConnectionsDefaultStart = VivaConnectionsDefaultStart,
                    IsVivaConnectionsDefaultStartPresent = true
                };
            }
            else if (hasDraftMode)
            {
                configurationParam = new()
                {
                    isInDraftMode = DraftMode,
                    IsInDraftModePresent = true
                };
            }
            else if (ParameterSpecified(nameof(DraftMode)))
            {
                configurationParam = new()
                {
                    isInDraftMode = DraftMode,
                    IsInDraftModePresent = true
                };
            }
            else
            {
                configurationParam = null;
            }

            if (Tenant.IsMultipleVivaConnectionsFlightEnabled)
            {
                if (Force || ShouldContinue("Before you update a home site or Viva Connections experiences, make sure you review the documentation at https://aka.ms/homesites.", Properties.Resources.Confirm))
                {
                    IEnumerable<TargetedSiteDetails> enumerable = Tenant.GetTargetedSitesDetails()?.Where((TargetedSiteDetails hs) => !hs.IsVivaBackendSite);
                    AdminContext.ExecuteQueryRetry();
                    if (enumerable == null || enumerable.Count() == 0)
                    {
                        Tenant.AddHomeSite(HomeSiteUrl, 1, null);
                        AdminContext.ExecuteQueryRetry();
                    }
                    else if (enumerable.Count() == 1 && !IsSameSiteUrl(enumerable.First().Url, HomeSiteUrl))
                    {
                        Tenant.RemoveTargetedSite(enumerable.First().SiteId);
                        AdminContext.ExecuteQueryRetry();
                        Tenant.AddHomeSite(HomeSiteUrl, 1, null);
                        AdminContext.ExecuteQueryRetry();
                    }
                    
                    ClientResult<TargetedSiteDetails> clientResult = Tenant.UpdateTargetedSite(HomeSiteUrl, configurationParam);

                    AdminContext.ExecuteQueryRetry();
                    WriteObject(clientResult.Value);
                }
            }
            else if (Force || ShouldContinue("Before you set a Home site, make sure you review the documentation at https://aka.ms/homesites.", Properties.Resources.Confirm))
            {
                Tenant.ValidateVivaHomeParameterExists(VivaConnectionsDefaultStart);
                ClientResult<string> clientResult = Tenant.SetSPHSiteWithConfiguration(HomeSiteUrl, configurationParam);
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