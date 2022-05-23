using System.Management.Automation;
using Microsoft.SharePoint.Client;
using web = Microsoft.SharePoint.Client.Web;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using PnP.PowerShell.Commands.Extensions;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPWeb")]
    [OutputType(typeof(void))]
    public class RemoveWeb : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public WebPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var web = Identity.GetWeb(ClientContext);
                web.EnsureProperties(w => w.Title, w => w.Url);
                if (Force || ShouldContinue(string.Format($"Remove web '{web.Title}' ({web.Url})"), Properties.Resources.Confirm))
                {
                    web.DeleteObject();
                    web.Context.ExecuteQueryRetry();
                }
            }
        }
    }
}