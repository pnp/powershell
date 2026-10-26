using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsLifecycle.Request, "PnPSyntexClassifyAndExtract")]
    [Obsolete("It no longer does anything, as SharePoint Syntex is no longer supported, and will be removed in a future version.")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class RequestSyntexClassifyAndExtract : BasePSCmdlet
    {
        const string ParameterSet_LIST = "List";
        const string Parameterset_FILE = "File";
        const string Parameterset_FOLDER = "Folder";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_LIST)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        public SwitchParameter Force = false;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        public SwitchParameter OffPeak = false;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_FILE)]
        public string FileUrl;

        [Parameter(Mandatory = false, ParameterSetName = Parameterset_FILE)]
        public PnPBatch Batch;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_FOLDER)]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = false, HelpMessage = "Ignored, as this cmdlet no longer performs any request. Kept for backwards compatibility with existing scripts.")]
        public PnPConnection Connection = null;
    }
}
