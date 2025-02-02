using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPListDesign", DefaultParameterSetName = ParameterSet_BYSITESCRIPTINSTANCE)]
    [OutputType(typeof(TenantListDesign))]
    public class AddListDesign : PnPSharePointOnlineAdminCmdlet
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
                SiteScriptIds = ParameterSpecified(nameof(SiteScriptIds)) ? SiteScriptIds : SiteScript.GetTenantSiteScript(Tenant).Select(sc => sc.Id).ToArray(),
                ThumbnailUrl = ThumbnailUrl,
                Title = Title
            };

            var design = Tenant.CreateListDesign(listDesignInfo);
            AdminContext.Load(design);
            AdminContext.ExecuteQueryRetry();
            WriteObject(design);
        }
    }
}