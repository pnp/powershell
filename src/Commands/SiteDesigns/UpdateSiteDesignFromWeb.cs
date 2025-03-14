using Microsoft.Online.SharePoint.TenantAdministration;
using System.Linq;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsData.Update, "PnPSiteDesignFromWeb", DefaultParameterSetName = ParameterSet_ALLCOMPONENTS)]
    [OutputType(typeof(TenantSiteDesign))]
    public class UpdateSiteDesignFromWeb : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_BASICCOMPONENTS = "Basic components";
        private const string ParameterSet_ALLCOMPONENTS = "All components";
        private const string ParameterSet_SPECIFICCOMPONENTS = "Specific components";

        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Url;        

        [Parameter(ParameterSetName = ParameterSet_ALLCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_SPECIFICCOMPONENTS)]
        [Parameter(ParameterSetName = ParameterSet_BASICCOMPONENTS)]
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
            // Retrieve the provided site design
            var siteDesigns = Identity.GetTenantSiteDesign(Tenant);

            // Ensure a site design has been found
            if(siteDesigns == null || siteDesigns.Length == 0)
            {
                throw new PSArgumentException("Site design provided through the Identity parameter could not be found. Use Add-PnPSiteDesignFromWeb if you intend on adding a new site design.", nameof(Identity));
            }

            // Ensure we only have one site design so we're sure which one needs to be updated
            if(siteDesigns.Length > 1)
            {
                throw new PSArgumentException("Multiple site designs have been found based on the name provided through the Identity parameter. Please use the site design Id instead to specify only one site design to update.", nameof(Identity));
            }
            var siteDesign = siteDesigns[0];

            // Generate site script
            LogDebug($"Generating site script from {Url}");

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
            AdminContext.ExecuteQueryRetry();

            var siteScript = generatedSiteScript.Value.JSON;

            // Retrieve the sitescripts linked to the site design
            siteDesign.EnsureProperty(d => d.SiteScriptIds);

            bool addAsNewSiteScript = false;
            if (siteDesign.SiteScriptIds.Length > 0)
            {
                // One or more site scripts exist in the site design
                if (siteDesign.SiteScriptIds.Length > 1)
                {
                    // Multiple site scripts in the site design
                    LogDebug($"Site design provided through the Identity parameter contains {siteDesign.SiteScriptIds.Length} site scripts. The first one will be overwritten with a new template from the site.");
                }
                else
                {
                    // One site script exists in the site design, which is the expected scenario
                    LogDebug($"Site design provided through the Identity parameter contains {siteDesign.SiteScriptIds.Length} site script. It will be overwritten with a new template from the site.");
                }

                // Update an existing site script
                try
                {
                    var script = Tenant.GetSiteScript(AdminContext, siteDesign.SiteScriptIds.First());
                    script.Content = siteScript;
                    Tenant.UpdateSiteScript(script);
                    AdminContext.ExecuteQueryRetry();
                }
                catch(Microsoft.SharePoint.Client.ServerException e) when (e.ServerErrorTypeName == "System.IO.FileNotFoundException")
                {
                    // Thrown when a site script is still referenced in the site design, but the actual site script has been removed. This likely means the site design is now in an orphaned state and cannot be used anymore. Going to try anyway.
                    LogDebug($"Site design provided through the Identity parameter contains a reference to site script {siteDesign.SiteScriptIds.First()} which no longer exists. Will try to add it as a new site script but it likely will fail as the site design is now orphaned. Remove the site design and create a new one if it keeps failing.");
                    addAsNewSiteScript = true;
                }
            }
            else
            {
                // No site scripts in the site design
                LogDebug($"Site design provided through the Identity parameter does not contain any site scripts yet. Adding a new site script to the site design.");
                addAsNewSiteScript = true;
            }
            
            if(addAsNewSiteScript)
            {
                // Add the site script as a new site script to the tenant
                TenantSiteScriptCreationInfo siteScriptCreationInfo = new TenantSiteScriptCreationInfo
                {
                    Title = siteDesign.Title,
                    Description = siteDesign.Description,
                    Content = siteScript
                };

                var addedSiteScript = Tenant.CreateSiteScript(siteScriptCreationInfo);
                AdminContext.Load(addedSiteScript);
                AdminContext.ExecuteQueryRetry();

                // Connect the site script to the site design
                siteDesign.SiteScriptIds = new[] { addedSiteScript.Id };
                Tenant.UpdateSiteDesign(siteDesign);
                AdminContext.ExecuteQueryRetry();
            } 

            WriteObject(siteDesign);
        }
    }
}