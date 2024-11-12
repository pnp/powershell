using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.New, "PnPLibraryFileVersionBatchDeleteJob")]
    public class NewLibraryFileVersionBatchDeleteJob : PnPWebCmdlet
    {
        private const string ParameterSet_AUTOMATICTRIM = "AutomaticTrim";
        private const string ParameterSet_DELETEOLDERTHANDAYS = "DeleteOlderThanDays";
        private const string ParameterSet_COUNTLIMITS = "CountLimits";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]        
        [ValidateNotNull]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_AUTOMATICTRIM)]
        public SwitchParameter Automatic;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DELETEOLDERTHANDAYS)]
        public int DeleteBeforeDays;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COUNTLIMITS)]
        public int MajorVersionLimit;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COUNTLIMITS)]
        public int MajorWithMinorVersionsLimit;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            FileVersionBatchDeleteMode batchDeleteMode;
            if (Automatic)
            {
                batchDeleteMode = FileVersionBatchDeleteMode.AutomaticTrim;
                DeleteBeforeDays = -1;
                MajorVersionLimit = -1;
                MajorWithMinorVersionsLimit = -1;
            }
            else if (ParameterSpecified(nameof(DeleteBeforeDays)))
            {
                batchDeleteMode = FileVersionBatchDeleteMode.DeleteOlderThanDays;
                MajorVersionLimit = -1;
                MajorWithMinorVersionsLimit = -1;
            }
            else if (ParameterSpecified(nameof(MajorVersionLimit)) ||
                ParameterSpecified(nameof(MajorWithMinorVersionsLimit)))
            {
                batchDeleteMode = FileVersionBatchDeleteMode.CountLimits;
                DeleteBeforeDays = -1;
            }
            else
            {
                throw new PSArgumentException($"One or more parameters issued cannot be used together or an insufficient number of parameters were provided. Specify Automatic for automatic trim. Specify DeleteBeforeDays for delete older than days. Specify MajorVersionLimit and MajorWithMinorVersionsLimit for version count limits.");
            }

            if (Force || ShouldContinue("By executing this command, versions specified will be permanently deleted. These versions cannot be restored from the recycle bin. Are you sure you want to continue?", Resources.Confirm))
            {
                var list = Identity.GetList(CurrentWeb);
                if (list != null)
                {
                    var ps = new FileVersionBatchDeleteParameters();

                    ps.BatchDeleteMode = batchDeleteMode;
                    ps.DeleteOlderThanDays = DeleteBeforeDays;
                    ps.MajorVersionLimit = MajorVersionLimit;
                    ps.MajorWithMinorVersionsLimit = MajorWithMinorVersionsLimit;

                    list.StartDeleteFileVersionsByMode(ps);

                    ClientContext.ExecuteQueryRetry();
                    
                    WriteObject("Success. Versions specified will be permanently deleted in the upcoming days.");
                }
            }
            else
            {
                WriteObject("Cancelled. No versions will be deleted.");
            }
        }
    }
}
