using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPRequestAccessEmails")]
    [OutputType(typeof(string))]
    public class GetRequestAccessEmails : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var emails = CurrentWeb.GetRequestAccessEmails();
            WriteObject(emails, true);
        }
    }
}