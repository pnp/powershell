using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPPageScheduling")]
    [OutputType(typeof(void))]
    public class DisablePageScheduling : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var pagesList = PagesUtility.GetModernPagesLibrary(PnPContext.Web);
            Utilities.REST.RestHelper.Post(Connection.HttpClient, $"{PnPContext.Web.Url}/_api/sitepages/pagesinlib(guid'{pagesList.Id}')/setscheduling(false)", ClientContext, null, "application/json", "application/json;odata=nometadata");
        }
    }
}
