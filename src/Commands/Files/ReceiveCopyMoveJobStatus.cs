using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommunications.Receive, "PnPCopyMoveJobStatus")]
    public class ReceiveCopyMoveJobStatus : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Model.CopyMigrationInfo Job;

        [Parameter(Mandatory = false)]
        public SwitchParameter Wait;

        protected override void ExecuteCmdlet()
        {
            Uri currentContextUri = new Uri(ClientContext.Url);
            var result = Utilities.CopyMover.GetCopyMigrationJobStatusAsync(HttpClient, currentContextUri, ClientContext, Job, !Wait).GetAwaiter().GetResult();
            WriteObject(result,true);
        }
    }
}
