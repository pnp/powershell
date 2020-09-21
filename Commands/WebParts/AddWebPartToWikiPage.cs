using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;
using PnP.Framework.Utilities;
using PnP.PowerShell.CmdletHelpAttributes;
using File = System.IO.File;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Add, "PnPWebPartToWikiPage")]
    public class AddWebPartToWikiPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "XML")]
        public string Xml = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "FILE")]
        public string Path = string.Empty;

        [Parameter(Mandatory = true)]
        public int Row;

        [Parameter(Mandatory = true)]
        public int Column;

        [Parameter(Mandatory = false)]
        public SwitchParameter AddSpace;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }


            WebPartEntity wp = null;

            switch (ParameterSetName)
            {
                case "FILE":
                    if (!System.IO.Path.IsPathRooted(Path))
                    {
                        Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                    }
                    if (File.Exists(Path))
                    {
                        var fileStream = new StreamReader(Path);
                        var webPartString = fileStream.ReadToEnd();
                        fileStream.Close();

                        wp = new WebPartEntity { WebPartXml = webPartString };
                    }
                    break;
                case "XML":
                    wp = new WebPartEntity { WebPartXml = Xml };
                    break;
            }
            if (wp != null)
            {
                SelectedWeb.AddWebPartToWikiPage(ServerRelativePageUrl, wp, Row, Column, AddSpace);
            }
        }
    }
}
