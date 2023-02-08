using System.Management.Automation;

using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPList", DefaultParameterSetName = ParameterSet_Delete)]
    [OutputType(typeof(void), ParameterSetName = new[] { ParameterSet_Delete })]
    [OutputType(typeof(RecycleResult), ParameterSetName = new[] { ParameterSet_Recycle })]
    public class RemoveList : PnPWebCmdlet
    {
        public const string ParameterSet_Delete = "Delete";
        public const string ParameterSet_Recycle = "Recycle";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Recycle)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter LargeList;
        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);
            if (list != null)
            {
                if (Force || ShouldContinue(Properties.Resources.RemoveList, Properties.Resources.Confirm))
                {
                    if (Recycle)
                    {
                        if (LargeList)
                        {
                            var operationId = list.StartRecycle();
                            ClientContext.ExecuteQueryRetry();
                            WriteObject($"Large List Operation Job {operationId.Value} initiated. It may take a while for this job to complete.");
                        }
                        else
                        { 
                            var recycleResult = list.Recycle();
                            ClientContext.ExecuteQueryRetry();
                            WriteObject(new RecycleResult { RecycleBinItemId = recycleResult.Value });
                        }
                    }
                    else
                    {
                        list.DeleteObject();
                        ClientContext.ExecuteQueryRetry();
                    }
                }
            }
        }
    }
}
