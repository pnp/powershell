using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsData.Unpublish, "PnPSyntexModel")]
    [Obsolete("It no longer does anything, as SharePoint Syntex is no longer supported, and will be removed in a future version.")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class UnPublishSyntexModel : BasePSCmdlet
    {
        const string ParameterSet_SINGLE = "Single";
        const string Parameterset_BATCHED = "Batched";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        public string ListWebUrl;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetSiteUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetWebServerRelativeUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetLibraryServerRelativeUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        [ValidateNotNull]
        public PnPBatch Batch;

        [Parameter(Mandatory = false, HelpMessage = "Ignored, as this cmdlet no longer performs any request. Kept for backwards compatibility with existing scripts.")]
        public PnPConnection Connection = null;
    }
}
