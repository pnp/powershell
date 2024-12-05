using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Get, "PnPWikiPageContent")]
    public class GetWikiPageContent : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position=0)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            WriteObject(CurrentWeb.GetWikiPageContent(ServerRelativePageUrl));
        }
    }
}
