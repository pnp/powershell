using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsLifecycle.Request, "PnPSyntexClassifyAndExtract")]
    [Obsolete(ObsoleteMessage)]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class RequestSyntexClassifyAndExtract : PnPWebCmdlet
    {
        private const string ObsoleteMessage = "Request-PnPSyntexClassifyAndExtract is obsolete and no longer does anything, as SharePoint Syntex is no longer supported. It will be removed in a future version.";

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

        protected override void ExecuteCmdlet()
        {
            LogWarning(ObsoleteMessage);
        }
    }
}
