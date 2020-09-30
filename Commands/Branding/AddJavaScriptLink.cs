using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Add, "PnPJavaScriptLink")]
    public class AddJavaScriptLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("Key")]
        public string Name = string.Empty;

        [Parameter(Mandatory = true)]
        public string[] Url = null;

        [Parameter(Mandatory = false)]
        public int Sequence = 0;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        protected override void ExecuteCmdlet()
        {
        
            switch (Scope)
            {
                case CustomActionScope.Web:
                    SelectedWeb.AddJsLink(Name, Url, Sequence);
                    break;

                case CustomActionScope.Site:
                    ClientContext.Site.AddJsLink(Name, Url, Sequence);
                    break;

                case CustomActionScope.All:
                    ThrowTerminatingError(new ErrorRecord(new Exception("Scope parameter can only be set to Web or Site"), "INCORRECTVALUE", ErrorCategory.InvalidArgument, this));
                    break;
            }
        }
    }
}
