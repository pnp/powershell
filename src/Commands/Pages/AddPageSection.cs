using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Add, "PnPPageSection")]
    [OutputType(typeof(void))]
    public class AddPageSection : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Page;

        [Parameter(Mandatory = true)]
        public CanvasSectionTemplate SectionTemplate;

        [Parameter(Mandatory = false)]
        public int Order = 1;

        [Parameter(Mandatory = false)]
        [ValidateRange(0, 3)]
        public int ZoneEmphasis = 0;

        [Parameter(Mandatory = false)]
        [ValidateRange(0, 3)]
        public int VerticalZoneEmphasis = 0;

        [Parameter(Mandatory = false)]
        public ZoneReflowStrategy ZoneReflowStrategy = ZoneReflowStrategy.TopToDown;

        protected override void ExecuteCmdlet()
        {
            var page = Page?.GetPage(Connection);

            if (page != null)
            {
                if (SectionTemplate == CanvasSectionTemplate.FlexibleLayoutSection || SectionTemplate == CanvasSectionTemplate.FlexibleLayoutVerticalSection)
                {
                    // Use the user-supplied ZoneReflowStrategy when adding flexible layout sections
                    page.AddSection(SectionTemplate, Order, ZoneEmphasis, VerticalZoneEmphasis, ZoneReflowStrategy);
                }
                else
                {
                    page.AddSection(SectionTemplate, Order, ZoneEmphasis, VerticalZoneEmphasis);
                }
                page.Save();
            }
            else
            {
                // If the client side page object cannot be found
                throw new Exception($"Page {Page} cannot be found.");
            }
        }
    }
}