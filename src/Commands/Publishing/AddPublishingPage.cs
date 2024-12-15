using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Add, "PnPPublishingPage")]
    public class AddPublishingPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("Name")]
        public string PageName = string.Empty;

        [Parameter(Mandatory = false)]
        [Alias("Folder")]
        public string FolderPath = string.Empty;

        [Parameter(Mandatory = true)]
        public string PageTemplateName = null;

        [Parameter(Mandatory = false, ParameterSetName = "WithTitle")]
        public string Title;

        [Parameter(Mandatory = false)]
        public SwitchParameter Publish;

        protected override void ExecuteCmdlet()
        {
            Folder pageFolder = null;
            if(!string.IsNullOrEmpty(FolderPath))
            {
                pageFolder = CurrentWeb.EnsureFolderPath(FolderPath);
            }

            switch (ParameterSetName)
            {
                case "WithTitle":
                    {
                        CurrentWeb.AddPublishingPage(PageName, PageTemplateName, Title, publish: Publish, folder: pageFolder);
                        break;
                    }
                default:
                    {
                        CurrentWeb.AddPublishingPage(PageName, PageTemplateName, publish: Publish, folder: pageFolder);
                        break;
                    }
            }
        }
    }
}
