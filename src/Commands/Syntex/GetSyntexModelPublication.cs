using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModelPublication")]
    [Obsolete("It no longer does anything, as SharePoint Syntex is no longer supported, and will be removed in a future version.")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class GetSyntexModelPublication : BasePSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        [Parameter(Mandatory = false, HelpMessage = "Ignored, as this cmdlet no longer performs any request. Kept for backwards compatibility with existing scripts.")]
        public PnPConnection Connection = null;
    }
}
