using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteDesign", SupportsShouldProcess = true)]
    public class SetSiteDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public TenantSiteDesignPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = false)]
        public GuidPipeBind[] SiteScriptIds;

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


        protected override void ExecuteCmdlet()
        {
            var design = Tenant.GetSiteDesign(ClientContext, Identity.Id);
            ClientContext.Load(design);
            ClientContext.ExecuteQueryRetry();
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
                if(ParameterSpecified(nameof(WebTemplate)))
                {
                    design.WebTemplate = ((int)WebTemplate).ToString();
                    isDirty = true;
                }
                if(ParameterSpecified(nameof(Version)))
                {
                    design.Version = Version;
                    isDirty = true;
                }
                if(ParameterSpecified(nameof(SiteScriptIds)))
                {
                    design.SiteScriptIds = SiteScriptIds.Select(t => t.Id).ToArray();
                    isDirty = true;
                }
                if (isDirty)
                {
                    Tenant.UpdateSiteDesign(design);
                    ClientContext.ExecuteQueryRetry();
                }
                WriteObject(design);
            } else
            {
                WriteError(new ErrorRecord(new ItemNotFoundException(), "SITEDESIGNNOTFOUND", ErrorCategory.ObjectNotFound, Identity));
            }
            
        }
    }
}