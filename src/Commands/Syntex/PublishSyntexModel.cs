using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsData.Publish, "PnPSyntexModel")]
    [Obsolete(ObsoleteMessage)]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class PublishSyntexModel : PnPWebCmdlet
    {
        private const string ObsoleteMessage = "Publish-PnPSyntexModel is obsolete and no longer does anything, as SharePoint Syntex is no longer supported. It will be removed in a future version.";

        const string ParameterSet_SINGLE = "Single";
        const string Parameterset_BATCHED = "Batched";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        public string ListWebUrl;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public string PublicationViewOption;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetSiteUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetWebServerRelativeUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        public string TargetLibraryServerRelativeUrl;

        [Parameter(Mandatory = true, ParameterSetName = Parameterset_BATCHED)]
        [ValidateNotNull]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            LogWarning(ObsoleteMessage);
        }
    }
}
