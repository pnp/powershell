using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Add, "PnPPageImageWebPart")]
    [OutputType(typeof(PnP.Core.Model.SharePoint.IPageWebPart))]
    public class AddPageImageWebPart : PnPWebCmdlet
    {
        private const string ParameterSet_DEFAULT = "Default";
        private const string ParameterSet_POSITIONED = "Positioned";

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_POSITIONED)]
        public PagePipeBind Page;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONED)]
        public string ImageUrl;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED)]
        public int Order = 1;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONED)]
        public int Section;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_POSITIONED)]
        public int Column;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED)]
        public PageImageAlignment PageImageAlignment = PageImageAlignment.Center;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED)]
        public int ImageWidth = 150;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED)]
        public int ImageHeight = 150;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED)]
        public string Caption = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED)]
        public string AlternativeText = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DEFAULT)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_POSITIONED)]
        public string Link = string.Empty;

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

            if (clientSidePage == null)
            {
                // If the client side page object cannot be found
                throw new Exception($"Page {Page} cannot be found.");
            }

            var imageControl = clientSidePage.GetImageWebPart(ImageUrl, new PageImageOptions()
            {
                Alignment = PageImageAlignment,
                Height = ImageHeight,
                Width = ImageWidth,
                Caption = Caption,
                AlternativeText = AlternativeText,
                Link = Link
            });

            if (ParameterSpecified(nameof(Section)))
            {
                if (ParameterSpecified(nameof(Section)))
                {
                    clientSidePage.AddControl(imageControl,
                    clientSidePage.Sections[Section - 1].Columns[Column - 1], Order);
                }
                else
                {
                    clientSidePage.AddControl(imageControl, clientSidePage.Sections[Section - 1], Order);
                }
            }
            else
            {
                clientSidePage.AddControl(imageControl, Order);
            }

            // Save the page
            clientSidePage.Save();

            WriteObject(imageControl);
        }
    }
}