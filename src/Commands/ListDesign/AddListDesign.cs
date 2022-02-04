using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPListDesign")]
    public class AddListDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public Guid[] SiteScriptIds;

        [Parameter(Mandatory = false)]
        public string Description;
        
        [Parameter(Mandatory = false)]
        public TenantListDesignIcon ListIcon;

        [Parameter(Mandatory = false)]
        public TenantListDesignColor ListColor;

        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        protected override void ExecuteCmdlet()
        {
            TenantListDesignCreationInfo listDesignInfo = new TenantListDesignCreationInfo
            {
                Description = Description,
                ListColor = ListColor,
                ListIcon = ListIcon,
                SiteScriptIds = SiteScriptIds,
                ThumbnailUrl = ThumbnailUrl,
                Title = Title
            };

            var design = Tenant.CreateListDesign(listDesignInfo);
            ClientContext.Load(design);
            ClientContext.ExecuteQueryRetry();
            WriteObject(design);
        }
    }
}