using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModelPublication")]
    [Obsolete(ObsoleteMessage)]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class GetSyntexModelPublication : PnPWebCmdlet
    {
        private const string ObsoleteMessage = "Get-PnPSyntexModelPublication is obsolete and no longer does anything, as SharePoint Syntex is no longer supported. It will be removed in a future version.";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        protected override void ExecuteCmdlet()
        {
            LogWarning(ObsoleteMessage);
        }
    }
}
