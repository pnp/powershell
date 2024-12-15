using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPAvailableLanguage")]
    [OutputType(typeof(Language))]
    public class GetAvailableLanguage : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(ClientContext.Web, w => w.RegionalSettings.InstalledLanguages);
            ClientContext.ExecuteQueryRetry();
            WriteObject(ClientContext.Web.RegionalSettings.InstalledLanguages, true);
        }
    }
}