using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;
using PnP.Framework.Utilities;

using File = System.IO.File;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Add, "PnPWebPartToWebPartPage")]
    public class AddWebPartToWebPartPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "XML")]
        public string Xml = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "FILE")]
        public string Path = string.Empty;

        [Parameter(Mandatory = true)]
        public string ZoneId;

        [Parameter(Mandatory = true)]
        public int ZoneIndex;

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

                        wp = new WebPartEntity {WebPartZone = ZoneId, WebPartIndex = ZoneIndex, WebPartXml = webPartString};
                    }
                    break;
                case "XML":
                    wp = new WebPartEntity {WebPartZone = ZoneId, WebPartIndex = ZoneIndex, WebPartXml = Xml};
                    break;
            }
            if (wp != null)
            {
                SelectedWeb.AddWebPartToWebPartPage(ServerRelativePageUrl, wp);
            }
        }
    }
}
