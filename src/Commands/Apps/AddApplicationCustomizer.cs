using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;
using PnP.PowerShell.Commands.Enums;
using System;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Add, "PnPApplicationCustomizer")]
    public class AddApplicationCustomizer : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Title = string.Empty;

        [Parameter(Mandatory = false)]
        public string Name = string.Empty;

        [Parameter(Mandatory = false)]
        public string Description = string.Empty;

        [Parameter(Mandatory = false)]
        public int Sequence = 0;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = true)]
        public Guid ClientSideComponentId;

        [Parameter(Mandatory = false)]
        public string ClientSideComponentProperties;

        [Parameter(Mandatory = false)]
        public string ClientSideHostProperties;

        protected override void ExecuteCmdlet()
        {
            CustomActionEntity ca = new CustomActionEntity
            {
                Title = Title,
                Name = Name,
                Description = Description,
                Sequence = Sequence,
                Location = "ClientSideExtension.ApplicationCustomizer",
                ClientSideComponentId = ClientSideComponentId,
                ClientSideComponentProperties = ClientSideComponentProperties,
                ClientSideHostProperties = ClientSideHostProperties
            };

            switch (Scope)
            {
                case CustomActionScope.Web:
                    CurrentWeb.AddCustomAction(ca);
                    break;

                case CustomActionScope.Site:
                    ClientContext.Site.AddCustomAction(ca);
                    break;

                case CustomActionScope.All:
                    WriteWarning("CustomActionScope 'All' is not supported for adding CustomActions");
                    break;
            }
        }
    }
}
