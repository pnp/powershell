using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModel")]
    [Obsolete("It no longer does anything, as SharePoint Syntex is no longer supported, and will be removed in a future version.")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class GetSyntexModel : BasePSCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "Ignored, as this cmdlet no longer performs any request. Kept for backwards compatibility with existing scripts.")]
        public PnPConnection Connection = null;
    }
}
