using System;
using System.Linq;
using System.Management.Automation;
using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Set, "PnPPageWebPart")]
    [OutputType(typeof(void))]
    public class SetClientSideWebPart : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Page;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public WebPartPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Title;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string PropertiesJson;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(Connection);

            if (clientSidePage == null)
                throw new Exception($"Page '{Page?.Name}' does not exist");

            var controls = Identity.GetWebPart(clientSidePage);
            if (controls.Any())
            {
                if (controls.Count > 1)
                {
                    throw new Exception("Found multiple webparts with the same name. Please use the InstanceId to retrieve the cmdlet.");
                }
                var webpart = controls.First();
                bool updated = false;

                if (ParameterSpecified(nameof(PropertiesJson)))
                {
                    (webpart as IPageWebPart).PropertiesJson = PropertiesJson;
                    updated = true;
                }
                if (ParameterSpecified(nameof(Title)))
                {
                    (webpart as IPageWebPart).Title = Title;
                    updated = true;
                }

                if (updated)
                {
                    clientSidePage.Save();
                }
            }
            else
            {
                throw new Exception($"Web part does not exist");
            }
        }
    }
}