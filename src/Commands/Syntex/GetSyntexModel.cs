using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModel")]
    [Obsolete(ObsoleteMessage)]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class GetSyntexModel : PnPWebCmdlet
    {
        private const string ObsoleteMessage = "Get-PnPSyntexModel is obsolete and no longer does anything, as SharePoint Syntex is no longer supported. It will be removed in a future version.";

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            LogWarning(ObsoleteMessage);
        }
    }
}
