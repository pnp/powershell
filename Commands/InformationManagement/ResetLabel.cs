using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Reset, "PnPLabel")]
    public class ResetLabel : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public bool SyncToItems;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(SelectedWeb);
            if (list != null)
            {
                var listUrl = list.RootFolder.ServerRelativeUrl;
                Microsoft.SharePoint.Client.CompliancePolicy.SPPolicyStoreProxy.SetListComplianceTag(ClientContext, listUrl, string.Empty, false, false, SyncToItems);

                try
                {
                    ClientContext.ExecuteQueryRetry();
                }
                catch (System.Exception error)
                {
                    WriteWarning(error.Message.ToString());
                }
            }
            else
            {
                WriteWarning("List or library not found.");
            }
        }
    }
}