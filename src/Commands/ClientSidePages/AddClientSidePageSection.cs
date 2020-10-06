using PnP.Framework.Pages;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ClientSidePages
{
    [Cmdlet(VerbsCommon.Add, "ClientSidePageSection")]
    public class AddClientSidePageSection : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ClientSidePagePipeBind Page;

        [Parameter(Mandatory = true)]
        public CanvasSectionTemplate SectionTemplate;

        [Parameter(Mandatory = false)]
        public int Order = 1;

        [Parameter(Mandatory = false)]
        public int ZoneEmphasis = 0;


        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page?.GetPage(ClientContext);

            if (clientSidePage != null)
            {
                clientSidePage.AddSection(new CanvasSection(clientSidePage, SectionTemplate, Order) { ZoneEmphasis = ZoneEmphasis });
                clientSidePage.Save();
            }
            else
            {
                // If the client side page object cannot be found
                throw new Exception($"Page {Page} cannot be found.");
            }

        }
    }
}