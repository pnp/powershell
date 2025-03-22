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
    [Cmdlet(VerbsCommon.Set, "PnPSiteDesign")]
    [OutputType(typeof(TenantSiteDesign))]
    public class SetSiteDesign : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = false)]
        public Guid[] SiteScriptIds;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsDefault;

        [Parameter(Mandatory = false)]
        public string PreviewImageAltText;

        [Parameter(Mandatory = false)]
        public string PreviewImageUrl;

        [Parameter(Mandatory = false)]
        public SiteWebTemplate WebTemplate;

        [Parameter(Mandatory = false)]
        public int Version;

        [Parameter(Mandatory = false)]
        public string ThumbnailUrl;

        [Parameter(Mandatory = false)]
        public Guid? DesignPackageId;

        protected override void ExecuteCmdlet()
        {
            var design = Tenant.GetSiteDesign(AdminContext, Identity.Id);
            AdminContext.Load(design);
            AdminContext.ExecuteQueryRetry();
            if (design != null)
            {
                var isDirty = false;
                if (ParameterSpecified(nameof(Title)))
                {
                    design.Title = Title;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(Description)))
                {
                    design.Description = Description;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(IsDefault)))
                {
                    design.IsDefault = IsDefault;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(PreviewImageAltText)))
                {
                    design.PreviewImageAltText = PreviewImageAltText;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(PreviewImageUrl)))
                {
                    design.PreviewImageUrl = PreviewImageUrl;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(WebTemplate)))
                {
                    design.WebTemplate = ((int)WebTemplate).ToString();
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(Version)))
                {
                    design.Version = Version;
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(SiteScriptIds)))
                {
                    design.SiteScriptIds = SiteScriptIds.Select(t => t).ToArray();
                    isDirty = true;
                }
                if (ParameterSpecified(nameof(ThumbnailUrl)))
                {
                    design.ThumbnailUrl = ThumbnailUrl;
                    isDirty = true;
                }
                if (DesignPackageId.HasValue)
                {
                    design.DesignPackageId = DesignPackageId.Value;
                    isDirty = true;
                }
                if (isDirty)
                {
                    Tenant.UpdateSiteDesign(design);
                    AdminContext.ExecuteQueryRetry();
                }
                WriteObject(design);
            }
            else
            {
                LogError(new ItemNotFoundException());
            }
        }
    }
}