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
        private const string ParameterSet_ALLLISTS = "All lists";
        private const string ParameterSet_SPECIFICCOMPONENTS = "Specific components";

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLLISTS)]
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

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLLISTS)]
        public SwitchParameter IncludeAllLists;        

        [Parameter(ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeBranding;
        
        [Parameter(ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeLinksToExportedItems;
        
        [Parameter(ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeRegionalSettings;
        
        [Parameter(ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeSiteExternalSharingCapability;

        [Parameter(ParameterSetName = ParameterSet_ALLLISTS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        public SwitchParameter IncludeTheme;

        protected override void ExecuteCmdlet()
        {
            // If no URL specified, we take the URL of the site that the current context is connected to
            if(!ParameterSpecified(nameof(Url)))
            {
                Url = Connection.Url;
            }

            if(IncludeAllLists || IncludeAll)
            {
                ClientContext.Load(ClientContext.Web.Lists, lists => lists.Where(list => !list.Hidden && !list.IsCatalog && !list.IsSystemList && !list.IsPrivate && !list.IsApplicationList && !list.IsSiteAssetsLibrary && !list.IsEnterpriseGalleryLibrary).Include(list => list.RootFolder.ServerRelativeUrl));
                ClientContext.ExecuteQueryRetry();

                Lists = ClientContext.Web.Lists.Select(l => System.Text.RegularExpressions.Regex.Replace(l.RootFolder.ServerRelativeUrl, @"\/(?:sites|teams)\/.*?\/", string.Empty)).ToArray();
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