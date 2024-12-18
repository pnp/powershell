
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Remove, "PnPPage", DefaultParameterSetName = ParameterSet_Delete)]
    [Alias("Remove-PnPClientSidePage")]
    [OutputType(typeof(void), ParameterSetName = new[] { ParameterSet_Delete })]
    [OutputType(typeof(RecycleResult), ParameterSetName = new[] { ParameterSet_Recycle })]
    public class RemovePage : PnPWebCmdlet
    {
        public const string ParameterSet_Delete = "Delete";
        public const string ParameterSet_Recycle = "Recycle";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Recycle)]
        public SwitchParameter Recycle;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue(Resources.RemoveClientSidePage, Resources.Confirm))
            {
                var clientSidePage = Identity.GetPage(Connection);
                if (clientSidePage == null)
                    throw new Exception($"Page '{Identity?.Name}' does not exist");

                if (Recycle.IsPresent)
                {
                    var recycleResult = clientSidePage.PageListItem.Recycle();
                    WriteObject(new RecycleResult { RecycleBinItemId = recycleResult });
                }
                else
                {
                    clientSidePage.Delete();
                }
            }
        }
    }
}