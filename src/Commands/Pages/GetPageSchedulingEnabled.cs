using System.Management.Automation;
using System.Text.Json;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Get, "PnPPageSchedulingEnabled")]
    [OutputType(typeof(void))]
    public class GetPageSchedulingEnabled : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var pagesList = PagesUtility.GetModernPagesLibrary(PnPContext.Web);
            var payload = new
            {
                parameters = new
                {
                    AddRequiredFields = false,
                    RenderOptions = 16385
                }
            };

            var results = Utilities.REST.RestHelper.Post<JsonElement>(Connection.HttpClient, $"{PnPContext.Web.Url}/_api/web/lists(guid'{pagesList.Id}')/RenderListDataAsStream", ClientContext, payload, false);

            var frameworkClientInfo = results.GetProperty("SPFrameworkClientInfo");
            var pageContextJson = frameworkClientInfo.GetProperty("PageContextJson");
            var value = pageContextJson.GetString();
            var contextElement = JsonDocument.Parse(value);
            var pageContextInfoElement = contextElement.RootElement.GetProperty("spPageContextInfo");
            var listPageScheduling = pageContextInfoElement.GetProperty("listPageSchedulingEnabled").GetBoolean();
            WriteObject(listPageScheduling);
        }
    }
}
