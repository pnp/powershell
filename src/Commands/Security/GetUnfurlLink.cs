using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Get, "PnPUnfurlLink")]
    public class GetUnfurlLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Url;

        protected override void ExecuteCmdlet()
        {
            var ctx = Connection.PnPContext;

            var unfurledResource = ctx.Web.UnfurlLink(Url);

            WriteObject(unfurledResource);
        }
    }
}
