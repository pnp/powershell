using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPMasterPage")]
    public class GetMasterPage : PnPWebCmdlet
    {

        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(CurrentWeb, w => w.MasterUrl, w => w.CustomMasterUrl);
            ClientContext.ExecuteQueryRetry();

            WriteObject(new {CurrentWeb.MasterUrl, CurrentWeb.CustomMasterUrl });
        }
    }
}
