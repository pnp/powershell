using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Add, "PnPPageWebPart")]
    [OutputType(typeof(PnP.Core.Model.SharePoint.IPageWebPart))]
    public class AddWebPart : PnPWebCmdlet
    {
        private const string ParameterSet_DEFAULTBUILTIN = "Default with built-in web part";
        private const string ParameterSet_DEFAULT3RDPARTY = "Default with 3rd party web part";
        private const string ParameterSet_POSITIONED3RDPARTY = "Positioned with 3rd party web part";
        private const string ParameterSet_POSITIONEDBUILTIN = "Positioned with built-in web part";
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_DEFAULTBUILTIN)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_DEFAULT3RDPARTY)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_POSITIONEDBUILTIN)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_POSITIONED3RDPARTY)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Page;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULTBUILTIN)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONEDBUILTIN)]
        public DefaultWebPart DefaultWebPartType;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT3RDPARTY)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONED3RDPARTY)]
        public PageComponentPipeBind Component;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULTBUILTIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT3RDPARTY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONEDBUILTIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED3RDPARTY)]
        public PropertyBagPipeBind WebPartProperties;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULTBUILTIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT3RDPARTY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONEDBUILTIN)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED3RDPARTY)]
        public int Order = 1;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONEDBUILTIN)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONED3RDPARTY)]
        public int Section;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONEDBUILTIN)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONED3RDPARTY)]
        public int Column;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Section)) && Section == 0)
            {
                throw new Exception("Section value should be at least 1 or higher");
            }

            if (ParameterSpecified(nameof(Column)) && Column == 0)
            {
                throw new Exception("Column value should be at least 1 or higher");
            }

            var clientSidePage = Page.GetPage(Connection);
            // If the client side page object cannot be found
            if (clientSidePage == null)
            {
                throw new Exception($"Page {Page} cannot be found.");
            }

            IPageWebPart webpart = null;
            if (ParameterSpecified(nameof(DefaultWebPartType)))
            {

                webpart = clientSidePage.InstantiateDefaultWebPart(DefaultWebPartType);
            }
            else
            {
                webpart = clientSidePage.NewWebPart(Component.GetComponent(clientSidePage));
            }

            if (WebPartProperties != null)
            {
                // TODO: do we still need this?
                if (WebPartProperties.Properties != null)
                {
                    webpart.PropertiesJson = Utilities.JSON.Merger.Merge(webpart.PropertiesJson, WebPartProperties.ToString());
                }
                else
                {
                    if (!string.IsNullOrEmpty(WebPartProperties.Json))
                    {
                        webpart.PropertiesJson = WebPartProperties.Json;
                    }
                }
            }

            if (ParameterSpecified(nameof(Section)))
            {
                if (ParameterSpecified(nameof(Column)))
                {
                    clientSidePage.AddControl(webpart,
                                clientSidePage.Sections[Section - 1].Columns[Column - 1], Order);
                }
                else
                {
                    clientSidePage.AddControl(webpart, clientSidePage.Sections[Section - 1], Order);
                }
            }
            else
            {
                clientSidePage.AddControl(webpart, Order);
            }

            clientSidePage.Save();
            WriteObject(webpart);
        }
    }
}