using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Add, "PnPJavaScriptBlock")]
    public class AddJavaScriptBlock : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name = string.Empty;

        [Parameter(Mandatory = true)]
        public string Script = null;

        [Parameter(Mandatory = false)]
        public int Sequence = 0;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        protected override void ExecuteCmdlet()
        {
            if (Scope != CustomActionScope.All)
            {
                if (Scope == CustomActionScope.Web)
                {
                    CurrentWeb.AddJsBlock(Name, Script, Sequence);
                }
                else
                {
                    var site = ClientContext.Site;
                    site.AddJsBlock(Name, Script, Sequence);
                }
            }
            else
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("Scope parameter can only be set to Web or Site"), "INCORRECTVALUE", ErrorCategory.InvalidArgument, this));
            }
        }
    }
}
