using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.ClientSidePages
{
    [Cmdlet(VerbsCommon.Set, "PnPClientSideWebPart")]
    public class SetClientSideWebPart : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ClientSidePagePipeBind Page;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ClientSideWebPartPipeBind Identity;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Title;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string PropertiesJson;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(ClientContext);

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
                    webpart.PropertiesJson = PropertiesJson;
                    updated = true;
                }
                if (ParameterSpecified(nameof(Title)))
                {
                    webpart.Title = Title;
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