using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Set, "PnPPageTextPart")]
    [OutputType(typeof(void))]
    public class SetClientSideText : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Page;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Guid InstanceId;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Text;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(Connection);

            if (clientSidePage == null)
                throw new Exception($"Page '{Page?.Name}' does not exist");

            var control = clientSidePage.Controls.FirstOrDefault(c => c.InstanceId == InstanceId);
            if (control != null)
            {
                var textControl = control as IPageText;
                textControl.Text = Text;
                clientSidePage.Save();
            }
            else
            {
                throw new Exception($"Component does not exist");
            }
        }
    }
}