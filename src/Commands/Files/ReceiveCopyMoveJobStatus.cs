using System;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.Client;

using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using File = Microsoft.SharePoint.Client.File;
using System.Net.Http;
using System.Text.Json;
using PnP.PowerShell.Commands.Model;

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
