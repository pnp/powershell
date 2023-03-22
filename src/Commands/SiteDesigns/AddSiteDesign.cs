using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteDesign", DefaultParameterSetName = ParameterSet_BYSITESCRIPTINSTANCE)]
    public class AddSiteDesign : PnPAdminCmdlet
    {
        private const string ParameterSet_BYSITESCRIPTIDS = "By SiteScript Ids";
        private const string ParameterSet_BYSITESCRIPTINSTANCE = "By SiteScript Instance";

        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYSITESCRIPTIDS)]
        public Guid[] SiteScriptIds;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYSITESCRIPTINSTANCE, ValueFromPipeline = true)]
        public TenantSiteScriptPipeBind SiteScript;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsDefault;

        [Parameter(Mandatory = false)]
        public string PreviewImageAltText;

        [Parameter(Mandatory = false)]
        public string PreviewImageUrl;

        [Parameter(Mandatory = true)]
        public SiteWebTemplate WebTemplate;

        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        [Parameter(Mandatory = false)]
        public Guid DesignPackageId;

        protected override void ExecuteCmdlet()
        {
            TenantSiteDesignCreationInfo siteDesignInfo = new TenantSiteDesignCreationInfo
            {
                Title = Title,
                SiteScriptIds = ParameterSpecified(nameof(SiteScriptIds)) ? SiteScriptIds : SiteScript.GetTenantSiteScript(Tenant).Select(sc => sc.Id).ToArray(),
                Description = Description,
                IsDefault = IsDefault,
                PreviewImageAltText = PreviewImageAltText,
                PreviewImageUrl = PreviewImageUrl,
                WebTemplate = ((int)WebTemplate).ToString(),
                ThumbnailUrl = ThumbnailUrl,
                DesignPackageId = DesignPackageId
            };

            var design = Tenant.CreateSiteDesign(siteDesignInfo);
            AdminContext.Load(design);
            AdminContext.ExecuteQueryRetry();
            WriteObject(design);
        }
    }
}