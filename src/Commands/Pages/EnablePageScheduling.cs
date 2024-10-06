using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPPageScheduling")]
    [OutputType(typeof(void))]
    public class EnablePageScheduling : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var pagesList = PagesUtility.GetModernPagesLibrary(Connection.PnPContext.Web);
            Utilities.REST.RestHelper.Post(Connection.HttpClient, $"{Connection.PnPContext.Web.Url}/_api/sitepages/pagesinlib(guid'{pagesList.Id}')/setscheduling(true)", ClientContext, null, "application/json", "application/json;odata=nometadata");
        }
    }
}
