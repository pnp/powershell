using Microsoft.Online.SharePoint.TenantAdministration;
using System.Linq;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteScriptFromWeb", DefaultParameterSetName = ParameterSet_BASICCOMPONENTS)]
    [OutputType(typeof(string))]
    public class GetSiteScriptFromWeb : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_BASICCOMPONENTS = "Basic components";
        private const string ParameterSet_ALLCOMPONENTS = "All components";
        private const string ParameterSet_ALLLISTS = "All lists";
        private const string ParameterSet_SPECIFICCOMPONENTS = "Specific components";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public string Url;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public string[] Lists;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        public SwitchParameter IncludeAll;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ALLLISTS)]
        public SwitchParameter IncludeAllLists;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeBranding;
        
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeLinksToExportedItems;
        
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeRegionalSettings;
        
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeSiteExternalSharingCapability;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeTheme;

        protected override void ExecuteCmdlet()
        {
            // If no URL specified, we take the URL of the site that the current context is connected to
            if(!ParameterSpecified(nameof(Url)))
            {
                Url = Connection.Url;
            }
            
            LogDebug($"Creating site script from web {Url}");

            if (IncludeAllLists || IncludeAll)
            {
                var targetWebContext = Url != Connection.Url ? Connection.CloneContext(Url) : ClientContext;

                targetWebContext.Load(targetWebContext.Web.Lists, lists => lists.Where(list => !list.Hidden && !list.IsCatalog && !list.IsSystemList && !list.IsPrivate && !list.IsApplicationList && !list.IsSiteAssetsLibrary && !list.IsEnterpriseGalleryLibrary).Include(list => list.RootFolder.ServerRelativeUrl));
                targetWebContext.ExecuteQueryRetry();

                Lists = targetWebContext.Web.Lists.Select(l => System.Text.RegularExpressions.Regex.Replace(l.RootFolder.ServerRelativeUrl, @"\/(?:sites|teams)\/.*?\/", string.Empty)).ToArray();

                LogDebug($"Including all custom lists and libraries in the site script... {Lists.Length} found");
                foreach (var list in Lists)
                {
                    LogDebug($"- {list}");
                }
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
            AdminContext.ExecuteQueryRetry();
            WriteObject(script.Value.JSON);
        }
    }
}