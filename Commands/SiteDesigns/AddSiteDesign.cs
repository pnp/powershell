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
    [Cmdlet(VerbsCommon.Add, "PnPSiteDesign")]
    public class AddSiteDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public GuidPipeBind[] SiteScriptIds;

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


        protected override void ExecuteCmdlet()
        {
            TenantSiteDesignCreationInfo siteDesignInfo = new TenantSiteDesignCreationInfo
            {
                Title = Title,
                SiteScriptIds = SiteScriptIds.Select(t => t.Id).ToArray(),
                Description = Description,
                IsDefault = IsDefault,
                PreviewImageAltText = PreviewImageAltText,
                PreviewImageUrl = PreviewImageUrl,
                WebTemplate = ((int)WebTemplate).ToString()
            };

            var design = Tenant.CreateSiteDesign(siteDesignInfo);
            ClientContext.Load(design);
            ClientContext.ExecuteQueryRetry();
            WriteObject(design);
        }
    }
}