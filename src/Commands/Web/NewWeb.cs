using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.New, "PnPWeb")]
    [OutputType(typeof(Web))]
    public class NewWeb : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = false)]
        public string Description = string.Empty;

        [Parameter(Mandatory = false)]
        public int Locale = 1033;

        [Parameter(Mandatory = true)]
        public string Template = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter BreakInheritance = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter InheritNavigation = true;
        protected override void ExecuteCmdlet()
        {
            var web = CurrentWeb.CreateWeb(Title, Url, Description, Template, Locale, !BreakInheritance,InheritNavigation);
            ClientContext.Load(web, w => w.Id, w => w.Url, w => w.ServerRelativeUrl);
            ClientContext.ExecuteQueryRetry();
            WriteObject(web);
        }

    }
}
