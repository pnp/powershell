using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using PnP.Framework.Entities;
using PnP.Framework.Utilities;
using File = System.IO.File;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Add, "PnPWebPartToWebPartPage")]
    [OutputType(typeof(WebPartDefinition))]
    public class AddWebPartToWebPartPage : PnPWebCmdlet
    {
        private const string ParameterSet_File = "File";
        private const string ParameterSet_Xml = "Xml";

        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Xml)]
        public string Xml = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_File)]
        public string Path = string.Empty;

        [Parameter(Mandatory = true)]
        public string ZoneId;

        [Parameter(Mandatory = true)]
        public int ZoneIndex;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }

            switch (ParameterSetName)
            {
                case ParameterSet_File:
                    if (!System.IO.Path.IsPathRooted(Path))
                    {
                        Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                    }

                    var webPartString = File.ReadAllText(Path);
                    AddWebPartToPage(webPartString);
                    break;

                case ParameterSet_Xml:
                    AddWebPartToPage(Xml);
                    break;
            }
        }

        private void AddWebPartToPage(string webPartXml)
        {
            var wp = new WebPartEntity { WebPartZone = ZoneId, WebPartIndex = ZoneIndex, WebPartXml = webPartXml };
            var webPartDef = CurrentWeb.AddWebPartToWebPartPage(ServerRelativePageUrl, wp);
            WriteObject(webPartDef);
        }
    }
}
