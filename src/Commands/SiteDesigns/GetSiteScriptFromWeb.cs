using Microsoft.Online.SharePoint.TenantAdministration;
using System.Linq;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteScriptFromWeb", DefaultParameterSetName = ParameterSet_BASICCOMPONENTS)]
    public class GetSiteScriptFromWeb : PnPAdminCmdlet
    {
        private const string ParameterSet_BASICCOMPONENTS = "Basic components";
        private const string ParameterSet_ALLCOMPONENTS = "All components";
        private const string ParameterSet_SPECIFICCOMPONENTS = "Specific components";

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Url;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false)]
        public string[] Lists;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        public SwitchParameter IncludeAll;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeBranding;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeLinksToExportedItems;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeRegionalSettings;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeSiteExternalSharingCapability;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeTheme;

        protected override void ExecuteCmdlet()
        {
            // If no URL specified, we take the URL of the site that the current context is connected to
            if(!ParameterSpecified(nameof(Url)))
            {
                Url = PnPConnection.Current.Url;
            }

            var tenantSiteScriptSerializationInfo = new TenantSiteScriptSerializationInfo
            {
                IncludeBranding = IncludeBranding || IncludeAll,
                IncludedLists = Lists?.Select(l => l.Replace("\\", "/")).ToArray(),
                IncludeLinksToExportedItems = IncludeLinksToExportedItems || IncludeAll,
                IncludeRegionalSettings = IncludeRegionalSettings || IncludeAll,
                IncludeSiteExternalSharingCapability = IncludeSiteExternalSharingCapability || IncludeAll,
                IncludeTheme = IncludeTheme || IncludeAll
            };
            var script = Tenant.GetSiteScriptFromSite(Url, tenantSiteScriptSerializationInfo);
            ClientContext.ExecuteQueryRetry();
            WriteObject(script.Value.JSON);
        }
    }
}