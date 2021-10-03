using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPSiteDesign")]
    public class AddSiteDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public Guid[] SiteScriptIds;

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
                SiteScriptIds = SiteScriptIds,
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