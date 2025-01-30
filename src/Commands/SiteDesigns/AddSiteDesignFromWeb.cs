using Microsoft.Online.SharePoint.TenantAdministration;
using System.Linq;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteDesignFromWeb", DefaultParameterSetName = ParameterSet_BASICCOMPONENTS)]
    [OutputType(typeof(TenantSiteDesign))]
    public class AddSiteDesignFromWeb : PnPSharePointOnlineAdminCmdlet
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

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false)]
        public SwitchParameter IsDefault;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false)]
        public string PreviewImageAltText;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false)]
        public string PreviewImageUrl;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false)]
        public Guid DesignPackageId;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = true)]
        public SiteWebTemplate WebTemplate;   

        protected override void ExecuteCmdlet()
        {
            // If no URL specified, we take the URL of the site that the current context is connected to
            if(!ParameterSpecified(nameof(Url)))
            {
                Url = Connection.Url;
            }

            // Generate site script
            var tenantSiteScriptSerializationInfo = new TenantSiteScriptSerializationInfo
            {
                IncludeBranding = IncludeBranding || IncludeAll,
                IncludedLists = Lists?.Select(l => l.Replace("\\", "/")).ToArray(),
                IncludeLinksToExportedItems = IncludeLinksToExportedItems || IncludeAll,
                IncludeRegionalSettings = IncludeRegionalSettings || IncludeAll,
                IncludeSiteExternalSharingCapability = IncludeSiteExternalSharingCapability || IncludeAll,
                IncludeTheme = IncludeTheme || IncludeAll
            };
            var generatedSiteScript = Tenant.GetSiteScriptFromSite(Url, tenantSiteScriptSerializationInfo);
            ClientContext.ExecuteQueryRetry();
            
            var siteScript = generatedSiteScript.Value.JSON;

            // Add the site script as a new site script to the tenant
            TenantSiteScriptCreationInfo siteScriptCreationInfo = new TenantSiteScriptCreationInfo
            {
                Title = Title,
                Description = Description,
                Content = siteScript
            };
            
            var addedSiteScript = Tenant.CreateSiteScript(siteScriptCreationInfo);
            ClientContext.Load(addedSiteScript);
            ClientContext.ExecuteQueryRetry();

            // Create a site design
            TenantSiteDesignCreationInfo siteDesignInfo = new TenantSiteDesignCreationInfo
            {
                Title = Title,
                SiteScriptIds = new [] { addedSiteScript.Id },
                Description = Description,
                IsDefault = IsDefault,
                PreviewImageAltText = PreviewImageAltText,
                PreviewImageUrl = PreviewImageUrl,
                WebTemplate = ((int)WebTemplate).ToString(),
                ThumbnailUrl = ThumbnailUrl,
                DesignPackageId = DesignPackageId
            };

            var design = Tenant.CreateSiteDesign(siteDesignInfo);
            ClientContext.Load(design);
            ClientContext.ExecuteQueryRetry();
            WriteObject(design);
        }
    }
}